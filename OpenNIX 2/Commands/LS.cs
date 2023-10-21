using System;
using System.IO;

namespace OpenNIX
{
    public static partial class Commands
    {
        public static void LS()
        {
            try
            {
                if (Directory.GetDirectories(Directory.GetCurrentDirectory()).Length > 0)
                    Console.Write(string.Join("  ", Directory.GetDirectories(Directory.GetCurrentDirectory())).Trim() + "  ", SVGAIIColor.Blue);
                if (Directory.GetFiles(Directory.GetCurrentDirectory()).Length > 0)
                    Console.Write(string.Join("  ", Directory.GetFiles(Directory.GetCurrentDirectory())).Trim(), SVGAIIColor.Red);
                Console.WriteLine();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to list directories. Error: {e.Message}"); 
            }
        }
    }
}
