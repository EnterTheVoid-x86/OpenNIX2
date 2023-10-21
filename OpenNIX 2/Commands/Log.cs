using Cosmos.Core;
using OpenNIX_2;
using System.IO;
using System.Text;
using System;

namespace OpenNIX
{
    public partial class Commands
    {
        public static void Log(string input, string[] args)
        {
            Console.WriteLine(input);
            Console.WriteLine(args[1] + args[2] + input);
            try
            {
                if (args[1] != "help")
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

                    switch (args[1])
                    {
                        case "info":
                            Logger.InfoLog(args[2]);
                            break;

                        case "success":
                            Logger.SuccessLog(args[2]);
                            break;

                        case "warn":
                            Logger.WarnLog(args[2]);
                            break;

                        case "fail":
                            Logger.ErrorLog(args[2]);
                            break;

                    }
                }
                else
                {
                    Console.WriteLine("log usage: log [info/success/warn/fail] [message]", SVGAIIColor.Gray);
                    Console.WriteLine("fail will initiate a kernel panic", SVGAIIColor.Gray);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Argument underflow.", SVGAIIColor.Red);
            }

        }
    }
}
