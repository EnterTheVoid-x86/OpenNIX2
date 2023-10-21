using IL2CPU.API.Attribs;
using System;
using static Cosmos.Core.INTs;
using OpenNIX_2;

namespace OpenNIX_Plugs
{
    [Plug(Target = typeof(Cosmos.Core.INTs))]
    public class INTs
    {
        /// <summary>
        /// Handles kernel exceptions (DIVIDE BY ZERO etc.)
        /// </summary>
        /// <param name="aDescription">Exception description</param>
        /// <param name="aName">Name of the exception</param>
        /// <param name="ctx">Cause of the exception</param>
        /// <param name="LastKnownAddressValue">Last known address in memory (Where in RAM the exception occurred)</param>
        public static void HandleException(uint aEIP, string aDescription, string aName, ref IRQContext ctx, uint LastKnownAddressValue = 0)
        {
            string error = ctx.Interrupt.ToString();
            const string xHex = "0123456789ABCDEF";

            string ctxinterrupt = "";
            ctxinterrupt = ctxinterrupt + xHex[(int)((ctx.Interrupt >> 4) & 0xF)];
            ctxinterrupt = ctxinterrupt + xHex[(int)(ctx.Interrupt & 0xF)];

            string LastKnownAddress = "";

            if (LastKnownAddressValue != 0)
            {
                LastKnownAddress = LastKnownAddress + xHex[(int)((LastKnownAddressValue >> 28) & 0xF)];
                LastKnownAddress = LastKnownAddress + xHex[(int)((LastKnownAddressValue >> 24) & 0xF)];
                LastKnownAddress = LastKnownAddress + xHex[(int)((LastKnownAddressValue >> 20) & 0xF)];
                LastKnownAddress = LastKnownAddress + xHex[(int)((LastKnownAddressValue >> 16) & 0xF)];
                LastKnownAddress = LastKnownAddress + xHex[(int)((LastKnownAddressValue >> 12) & 0xF)];
                LastKnownAddress = LastKnownAddress + xHex[(int)((LastKnownAddressValue >> 8) & 0xF)];
                LastKnownAddress = LastKnownAddress + xHex[(int)((LastKnownAddressValue >> 4) & 0xF)];
                LastKnownAddress = LastKnownAddress + xHex[(int)(LastKnownAddressValue & 0xF)];
            }
            FatalError.Crash(aName, aDescription, LastKnownAddress, ctx.ToString());
        }
    }
    class FatalError
    {
        public static SVGAIITerminal Console = Kernel.Console;
        public static string ErrorSplash = @$"[ FAIL ]: panic

OpenNIX version {OpenNIX_2.Kernel.Version} build {OpenNIX_2.Kernel.Build}
Fatal Panic!

Error information can be found below:";

        public static void Crash(string exception, string description, string lastknownaddress, string ctxinterrupt)
        {
            Console.BackgroundColor = SVGAIIColor.DarkRed;
            Console.Clear();
            Console.WriteLine(ErrorSplash);
            Console.WriteLine("CPU Exception: " + ctxinterrupt);
            Console.WriteLine("Exception: " + exception);
            Console.WriteLine("Exception description: " + description);
            if (lastknownaddress != "")
            {
                Console.WriteLine("Last known address: " + lastknownaddress);
            }
            Console.WriteLine("\n\n\n\nPress any key to restart...");
            Console.ReadKey();
            Cosmos.System.Power.Reboot();
        }
    }
}