/* This code is licensed under the ekzFreeUse license
 * If a license wasn't included with the program,
 * refer to https://github.com/EnterTheVoid-x86/OpenNIX2/blob/master/LICENSE.md */


using Cosmos.System;
using PrismAPI.Filesystem.Formats.ELF.ELFHeader;
using PrismAPI.Runtime.SSharp;
using PrismAPI.Runtime;
using System;
using System.IO;

namespace OpenNIX
{
    public static unsafe class Shell
    {
        public static void Run(string input, SVGAIITerminal Console)
        {
            Console.Font = Resources.Font;
            Commands.Console = Console;

            input = input.Trim();
            string[] args = input.Split(' ');

            switch (args[0].Trim().ToLower())
            {
                case "":
                    break;

                case "?":
                    Commands.Help();
                    break;

                case "help":
                    Commands.Help();
                    break;

                case "info":
                    TimeSpan upTime = DateTime.Now - OpenNIX_2.Kernel.BootTime;
                    Commands.Info();
                    Console.Write(upTime, SVGAIIColor.Gray);
                    break;

                case "log":
                    Commands.Log(input, args);
                    break;

                case "clear":
                    Commands.Clear();
                    break;

                case "reboot":
                    Commands.Reboot();
                    break;

                case "shutdown":
                    Commands.Shutdown();
                    break;

                case "crashsystem":
                    throw new System.Exception("User initiated crash.");

                case "setfont":
                    Commands.SetFont(input, args);
                    break;

                case "setbackground":
                    Commands.SetBackground(input, args);
                    break;

                case "hash":
                    Commands.Hash(input.Split("hash ")[1].Split(" >> "));
                    break;

                case "onvi":
                    Commands.StartONVI(args);
                    break;

                case "license":
                    Commands.License();
                    break;

                case "script":
                    Commands.Script(input);
                    break;

                case "adduser":
                    Commands.AddUser(input, args);
                    break;

                case "gui":
                    //Console.Clear();
                    //System.Threading.Thread.Sleep(2000);
                    //Commands.GUITest();
                    GUI.WindowManager.Start();
                    break;

                case "setres":
                    Commands.SetResolution();
                    break;

                case "who":
                    Commands.Who();
                    break;

                case "ls":
                    if (input == "ls")
                    {
                        args[0] = Directory.GetCurrentDirectory();
                        Commands.LS(args);
                    }
                    else
                    {
                        Commands.LS(input.Split("ls ")[1].Split(" >> "));
                    }
                    break;

                case "stopgui":
                    GUI.WindowManager.stopped = "true";
                    break;

                case "cd":
                    Commands.CD(input, args);
                    break;

                case "cp":
                    Commands.Copy(input, args);
                    break;

                case "touch":
                    Commands.Touch(input, args);
                    break;

                case "mkdir":
                    Commands.MkDir(input, args);
                    break;

                case "cat":
                    Commands.Cat(input, args);
                    break;

                case "rm":
                    Commands.RM(input, args);
                    break;

                case "rmdir":
                    Commands.RmDir(input, args);
                    break;

                case "echo":
                    Commands.Echo(input.Split("echo ")[1].Split(" >> "));
                    break;

                case "calc":
                    Commands.Calculator(input.Split("calc ")[1].Split(" >> "));
                    break;

                case "ss":
                    Commands.SystemSharp(input);
                    break;

                default:
                    if (File.Exists($@"0:\{args[0]}") || File.Exists(args[0]) || File.Exists($@"{Directory.GetCurrentDirectory()}\{args[0]}"))
                    {
                        byte[] ROM = null;
                        BinarySS EXE = null;
                        // Try to read the program's running data.

                        try
                        {
                            ROM = File.ReadAllBytes(args[0]);
                        }
                        catch (FileNotFoundException) 
                        {
                            try
                            {
                                ROM = File.ReadAllBytes($@"0:\{args[0]}");
                            }
                            catch (FileNotFoundException)
                            {
                                ROM = File.ReadAllBytes($@"{Directory.GetCurrentDirectory()}\{args[0]}");
                            }
                        }

                        // Check if the file isn't an ELF. Run as a SSharp program if it isn't.
                        if (ROM.Length < sizeof(ELFHeader32))
                        {
                            try
                            {
                                EXE = new(File.ReadAllBytes(args[0]));
                            }
                            catch (FileNotFoundException)
                            {
                                try
                                {
                                    EXE = new(File.ReadAllBytes($@"0:\{args[0]}"));
                                }
                                catch (FileNotFoundException) 
                                {
                                    EXE = new(File.ReadAllBytes(($@"{Directory.GetCurrentDirectory()}\{args[0]}")));
                                }
                            }

                            while (EXE.IsEnabled)
                            {
                                EXE.NextInstruction();
                            }

                            return;
                        }
                        // Run an elf file when it's detected.
                        else
                        {
                            // Create a new header, then run it.
                            Executable E = Executable.FromELF32(ROM);
                            E.Main();
                            return;
                        }
                    }
                    Console.WriteLine($"Command \"{args[0].Trim()}\" not found.", SVGAIIColor.Red);
                    break;
            }
        }
    }
}