using System;
using System.IO;

namespace OpenNIX
{
    public static partial class Commands
    {
        public static void CD(string input, string[] args)
        {
            if (!input.Contains('"') && args.Length < 2)
            {
                Console.WriteLine("Argument underflow.", SVGAIIColor.Red);
                return;
            }
            else if (!input.Contains('"') && args.Length > 2)
            {
                Console.WriteLine("Argument overflow.", SVGAIIColor.Red);
                return;
            }

            args[1] = args[1].Replace("/", "\\");

            string cdTo = string.Empty;

            if (args[1] == "..")
            {
                try
                {
                    cdTo = Directory.GetCurrentDirectory().TrimEnd('\\').Remove(Directory.GetCurrentDirectory().LastIndexOf('\\') + 1);
                    cdTo = cdTo.Remove(cdTo.Length - 1);
                }
                catch (Exception e) { Console.WriteLine($"Error: {e.Message}"); }

                if (!cdTo.StartsWith(@"0:\"))
                    cdTo = @"0:\"; // Directory error correction
            }
            else
            {
                if (input.Contains("\""))
                    try
                    {
                        cdTo = @$"{Directory.GetCurrentDirectory()}{(Directory.GetCurrentDirectory() != @"0:\" ? @"\" : "")}{input.Substring(4, input.Length - 5)}".Trim();
                    }
                    catch (Exception e)
                    { Console.WriteLine($"Error: {e.Message}"); }
                else
                    try
                    {
                        cdTo = @$"{Directory.GetCurrentDirectory()}{(Directory.GetCurrentDirectory() != @"0:\" ? @"\" : "")}{args[1]}".Trim();
                    }
                    catch (Exception e) { Console.WriteLine($"Error: {e.Message}"); }
            }


            try
            {
                if (Directory.Exists(cdTo))
                    Directory.SetCurrentDirectory(cdTo);
                else
                    Console.WriteLine($"cd: No such directory \"{cdTo.Substring(cdTo.LastIndexOf("\\") + 1)}\"", SVGAIIColor.Red);
            }
            catch (Exception e) { Console.WriteLine($"Error: {e.Message}"); }
        }
    }
}
