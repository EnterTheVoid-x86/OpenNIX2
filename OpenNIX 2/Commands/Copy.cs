using System;
using System.IO;

namespace OpenNIX
{
    public static partial class Commands
    {
        public static void Copy(string input, string[] args)
        {
            if (!input.Contains('"') && args.Length < 3)
            {
                Console.WriteLine("Argument underflow.", SVGAIIColor.Red);
                return;
            }
            else if (!input.Contains('"') && args.Length > 3)
            {
                Console.WriteLine("Argument overflow.", SVGAIIColor.Red);
                return;
            }

            args[1] = args[1].Replace("/", "\\");
            args[2] = args[2].Replace("/", "\\");

            string fileTo = string.Empty;
            string fileCp = string.Empty;

            try
            {
                if (input.Contains("\""))
                {
                    fileTo = @$"{Directory.GetCurrentDirectory()}{(Directory.GetCurrentDirectory() != @"0:\" ? @"\" : "")}{input.Substring(4, input.Length - 5)}".Trim();
                    Console.WriteLine(fileTo);
                }
                else
                {
                    fileTo = @$"{Directory.GetCurrentDirectory()}{(Directory.GetCurrentDirectory() != @"0:\" ? @"\" : "")}{args[1]}".Trim();
                    fileCp = @$"{Directory.GetCurrentDirectory()}{(Directory.GetCurrentDirectory() != @"0:\" ? @"\" : "")}{args[2]}".Trim();
                }
            }
            catch (Exception e) { Console.WriteLine($"Error: {e.Message}"); }
            try
            {
                File.Copy(fileTo, fileCp);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"copy: Failed to copy file \"{fileTo}\": {ex.Message}", SVGAIIColor.Red);
                return;
            }
        }
    }
}
