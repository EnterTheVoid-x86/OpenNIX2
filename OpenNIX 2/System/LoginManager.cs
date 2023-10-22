using OpenNIX_2;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace OpenNIX
{
    public static class LoginManager
    {
        public static void Run(SVGAIITerminal Console)
        {
            Console.Font = Resources.Font;
            string[] userInfo = null;
            Kernel.Hostname = File.ReadAllText(@"0:\etc\hostname");
            if (Directory.Exists(@"0:\home\"))
            {
                Console.DrawImage(Resources.Logo, false);
                Console.WriteLine($"Callux OpenNIX {Kernel.Version} build {Kernel.Build}", SVGAIIColor.Gray);
                Console.Write($"{Kernel.Hostname} login: ",SVGAIIColor.Gray);
                var username = Console.ReadLine();
                if (Directory.Exists($@"0:\home\{username}"))
                {
                    Console.Write("Password: ", SVGAIIColor.Gray);
                    string password = Console.ReadLine(true);

                    string passTableString = File.ReadAllText(@"0:\etc\shadow");

                    passTableString.Trim();

                    string[] passTable = passTableString.Split("/");

                    for (int i = 0; i < passTable.Length; i++)
                    {
                        // The lines commented out here are only for debug purposes.
                        //Console.WriteLine(passTable[i]);
                        if (passTable[i].StartsWith(username))
                        {
                            //Console.WriteLine(passTable[i]);
                            userInfo = passTable[i].Split(":");
                            //for ( i = 0; i < userInfo.Length; i++)
                            //{
                            //    Console.WriteLine(userInfo[i]);
                            //}
                            break;
                        }
                        if (passTable.Length == i + 1)
                        {
                            Logger.ErrorLog("Failed to find entry for user in /etc/shadow!");
                        }
                    }

                    if (Hashing.GetNonRandomizedHashCode(password).ToString() == userInfo[1])
                    {
                        Logger.InfoLog("Dropping into Shell...");
                        Kernel.Username = username;
                        Directory.SetCurrentDirectory(DiskManager.GetCosmosLikePath($"/home/{username}"));
                        for (; ; )
                        {
                            Console.Write($"[{Kernel.Username}@{Kernel.Hostname} {DiskManager.GetUnixLikePath(Directory.GetCurrentDirectory())}]# ", SVGAIIColor.Gray);
                            var input = Console.ReadLine();
                            if (input == "exit")
                            {
                                break;
                            }
                            Shell.Run(input, Console);
                        }
                    }
                    else
                    {
                        Console.WriteLine("login: incorrect password", SVGAIIColor.Gray);
                    }

                }
                else
                {
                    Console.WriteLine("login: no such user exists", SVGAIIColor.Gray);
                }
            }
            else
            {
                Console.WriteLine("Welcome to OpenNIX Setup.", SVGAIIColor.Gray);
                Console.WriteLine("\nWe'll guide you through setting up your computer.", SVGAIIColor.Gray);
                Console.Write("First off, what do you want your username to be? ", SVGAIIColor.Gray);
                var newUsername = Console.ReadLine();
                Console.Write("Next, choose a password: ", SVGAIIColor.Gray);
                var newPassword = Console.ReadLine(true);
                Console.Write("Finally, choose a hostname for your computer: ", SVGAIIColor.Gray);
                var newHostname = Console.ReadLine();

                File.WriteAllText($@"0:\etc\hostname", newHostname);
                File.WriteAllText($@"0:\etc\shadow", $"{newUsername}:{Hashing.GetNonRandomizedHashCode(newPassword).ToString()}");

                Directory.CreateDirectory($@"0:\home\");
                Directory.CreateDirectory($@"0:\home\{newUsername}\");

                Commands.Reboot();
            }
        }

    }
}
