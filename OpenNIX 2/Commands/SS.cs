using PrismAPI.Runtime.SSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XSharp.Assembler.x86;

namespace OpenNIX
{
    public partial class Commands
    {
        public static void SystemSharp(string shinput)
        {
            string[] args = shinput.Split(' ');

            if (args.Length == 1)
            {
                Console.WriteLine("OpenSS CLI interface, v2.1");
                Console.WriteLine("Copyleft Callux 2023.\n");

                while (true)
                {
                    try
                    {
                        Console.Write(">>> ");

                        string input = Console.ReadLine();

                        if (string.IsNullOrEmpty(input))
                        {
                            continue;
                        }

                        switch (input)
                        {
                            case "Exit();":
                                return;
                            default:
                                BinarySS EXE = CompilerSS.Compile(input);

                                while (EXE.IsEnabled)
                                {
                                    EXE.NextInstruction();
                                }
                                break;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }

            if (args[1] == "compile")
            {
                Console.WriteLine(args.Length);
                if (args.Length < 4)
                {
                    Console.WriteLine("Argument underflow.", SVGAIIColor.Red);
                    return;
                }

                Console.WriteLine($"Compiling {args[2]}...");

                args[2] = args[2].Replace("/", "\\");

                string catTo = string.Empty;

                try
                {
                    if (shinput.Contains("\""))
                        catTo = @$"{Directory.GetCurrentDirectory()}{(Directory.GetCurrentDirectory() != @"0:\" ? @"\" : "")}{shinput.Substring(5, shinput.Length - 5)}".Trim();
                    else if (args[2].StartsWith("\""))
                        catTo = @$"0:\{args[2]}";
                    else
                        catTo = @$"{Directory.GetCurrentDirectory()}{(Directory.GetCurrentDirectory() != @"0:\" ? @"\" : "")}{args[2]}".Trim();
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Failed to get directories. Error: {e.Message}", SVGAIIColor.Red);
                    return;
                }

                if (File.Exists(catTo))
                {
                    byte[] file = File.ReadAllBytes(catTo);
                    BinarySS EXE = CompilerSS.Compile(Encoding.ASCII.GetString(file));
                    File.WriteAllBytes(@$"{Directory.GetCurrentDirectory()}{(Directory.GetCurrentDirectory() != @"0:\" ? @"\" : "")}{args[3]}".Trim(), ((MemoryStream)EXE.ROM.BaseStream).ToArray());
                }
                else
                {
                    Console.WriteLine("That file doesn't exist!", SVGAIIColor.Red);
                    return;
                }
            }

            if (args[1] == "dump")
            {
                if (File.Exists(@$"{Directory.GetCurrentDirectory()}{(Directory.GetCurrentDirectory() != @"0:\" ? @"\" : "")}{args[2]}".Trim()))
                {
                    new BinarySS(File.ReadAllBytes(@$"{Directory.GetCurrentDirectory()}{(Directory.GetCurrentDirectory() != @"0:\" ? @"\" : "")}{args[2]}".Trim())).DumpInstruction();
                }
                else
                {
                    Console.WriteLine("That file doesn't exist!", SVGAIIColor.Red);
                }
                return;
            }

        }
    }
}
