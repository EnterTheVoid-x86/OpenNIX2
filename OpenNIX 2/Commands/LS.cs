using Cosmos.System.FileSystem.VFS;
using System;
using System.IO;

namespace OpenNIX
{
    public static partial class Commands
    {
        public static void LS(string[] args)
        {
            try
            {
                args[0] = args[0].Replace("/", "0:\\");
                if (args[0].StartsWith("0:\\"))
                {
                    if (Directory.GetDirectories(args[0]).Length > 0)
                        Console.Write(string.Join("  ", Directory.GetDirectories(args[0])).Trim() + "  ", SVGAIIColor.Blue);
                    if (Directory.GetFiles(args[0]).Length > 0)
                        Console.Write(string.Join("  ", Directory.GetFiles(args[0])).Trim(), SVGAIIColor.Red);
                    Console.WriteLine();
                }
                else
                {
                    if (Directory.GetDirectories(Directory.GetCurrentDirectory()).Length > 0)
                        Console.Write(string.Join("  ", Directory.GetDirectories(Directory.GetCurrentDirectory())).Trim() + "  ", SVGAIIColor.Blue);
                    if (Directory.GetFiles(Directory.GetCurrentDirectory()).Length > 0)
                        Console.Write(string.Join("  ", Directory.GetFiles(Directory.GetCurrentDirectory())).Trim(), SVGAIIColor.Red);
                    Console.WriteLine();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to list directories. Error: {e.Message}", SVGAIIColor.Red); 
            }
        }
    }
}
