using Cosmos.System;

namespace OpenNIX
{
    public static partial class Commands
    {
        public static void Reboot()
        {
            Console.WriteLine("reboot: Rebooting...");
            Power.Reboot();
        }
    }
}
