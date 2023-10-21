﻿using System;
using System.IO;

namespace OpenNIX
{
    public static partial class Commands
    {
        public static void MkDir(string input, string[] args)
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

            string dirTo = string.Empty;

            try
            {
                if (input.Contains("\""))
                    dirTo = @$"{Directory.GetCurrentDirectory()}{(Directory.GetCurrentDirectory() != @"0:\" ? @"\" : "")}{input.Substring(4, input.Length - 5)}".Trim();
                else
                    dirTo = @$"{Directory.GetCurrentDirectory()}{(Directory.GetCurrentDirectory() != @"0:\" ? @"\" : "")}{args[1]}".Trim();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
            try
            {
                Directory.CreateDirectory(dirTo);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"mkdir: Failed to create directory: {ex.Message}", SVGAIIColor.Red);
                return;
            }
        }
    }
}
