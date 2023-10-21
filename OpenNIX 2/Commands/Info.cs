using Cosmos.Core;
using OpenNIX_2;

namespace OpenNIX
{
    public partial class Commands
    {
        static uint maxmem = CPU.GetAmountOfRAM();
        static ulong availableMem = GCImplementation.GetAvailableRAM();
        static ulong usedmem = maxmem - availableMem;
        public static void Info(string currentTime)
        {
            Console.WriteLine();
            Console.DrawImage(Resources.Logo, false);
            Console.WriteLine($"\nOpenNIX Version {Kernel.Version}\nBuild {Kernel.Build}\n{Kernel.Copyright}\n", SVGAIIColor.Gray);
            Console.WriteLine("System Specifications:", SVGAIIColor.Gray);
            Console.WriteLine($"CPU: {CPU.GetCPUBrandString()}", SVGAIIColor.Gray);
            Console.WriteLine($"RAM: {usedmem}/{maxmem}MB", SVGAIIColor.Gray);
            Console.WriteLine($"Display: {Kernel.Screen.Width}x{Kernel.Screen.Height}", SVGAIIColor.Gray);
            Console.WriteLine($"Time at boot: {Kernel.BootTime}", SVGAIIColor.Gray);
            Console.WriteLine($"Current time: {currentTime}", SVGAIIColor.Gray);
        }
    }
}
