using OpenNIX.GUI.Apps;
using OpenNIX_2;
using PrismAPI.Graphics;
using System;
using System.IO;

namespace OpenNIX
{
    public partial class Commands
    {
        public static void SetBackground(string input, string[] args)
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

            string backgroundTo = input.Contains("\"") ? @$"{Directory.GetCurrentDirectory()}{(Directory.GetCurrentDirectory() != @"0:\" ? @"\" : "")}{input.Substring(5, input.Length - 5)}".Trim() : @$"{Directory.GetCurrentDirectory()}{(Directory.GetCurrentDirectory() != @"0:\" ? @"\" : "")}{args[1]}".Trim();

            try
            {
                Desktop.BackgroundImage = Image.FromBitmap(File.ReadAllBytes(backgroundTo), false);
                Desktop.BackgroundChangeRequest = true;
                File.WriteAllText(@$"0:\home\{Kernel.Username}\background.txt", backgroundTo);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"setbackground: Failed to read file: {ex.Message}", SVGAIIColor.Red);
                return;
            }
        }
    }
}
