using IL2CPU.API.Attribs;
using System;
using System.IO;

namespace OpenNIX
{
    public static partial class Commands
    {
        [ManifestResourceStream(ResourceName = "OpenNIX_2.Resources.DefaultFont.btf")] public static byte[] defaultFont;
        public static void SetFont(string input, string[] args)
        {
            Console.WriteLine($"setfont: warning: setfont is untested, use at your own risk", SVGAIIColor.Gray);
            Console.WriteLine($"setfont: must be run twice to set font", SVGAIIColor.Gray);

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

            if (args[1] == "default")
            {
                Console.WriteLine("Set font to default.", SVGAIIColor.Gray);
                Resources.rawFont = defaultFont;
                Resources.GenerateFont();
            }

            args[1] = args[1].Replace("/", "\\");

            string fontTo = string.Empty;

            try
            {
                if (args[1] != "default")
                {
                    if (input.Contains("\""))
                        fontTo = @$"{Directory.GetCurrentDirectory()}{(Directory.GetCurrentDirectory() != @"0:\" ? @"\" : "")}{input.Substring(4, input.Length - 5)}".Trim();
                    else
                        fontTo = @$"{Directory.GetCurrentDirectory()}{(Directory.GetCurrentDirectory() != @"0:\" ? @"\" : "")}{args[1]}".Trim();
                }
            }
            catch (Exception e) { Console.WriteLine($"Error: {e.Message}", SVGAIIColor.Red); }
            try
            {
                if (args[1] != "default")
                {
                    Resources.rawFont = File.ReadAllBytes(fontTo);
                    Resources.GenerateFont();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"setfont: Failed to set font \"{fontTo}\": {ex.Message}", SVGAIIColor.Red);
                return;
            }
        }
    }
}
