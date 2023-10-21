using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static Spruce.Tokens.Token;

namespace OpenNIX
{
    public partial class Commands
    {
        public static string scriptCommands = string.Empty;
        public static bool stopRunning = false;
        public static void Script(string input)
        {
            string[] file = input.Split(" ");
            Console.WriteLine(Directory.GetCurrentDirectory() + "\\" + file[1]);
            if (!File.Exists(Directory.GetCurrentDirectory() + "\\" + file[1]) || !File.Exists(file[1]))
            {
                Console.WriteLine("script: File does not exist.", SVGAIIColor.Red);
                stopRunning = true;
            }
            if (!stopRunning == true)
            {
                try
                {
                    if (File.Exists(Directory.GetCurrentDirectory() + "\\" + file[1]))
                    {

                        scriptCommands = File.ReadAllText(Directory.GetCurrentDirectory() + "\\" + file[1]);
                    }
                    else if (File.Exists(input))
                    {
                        scriptCommands = File.ReadAllText(input);
                    }

                    scriptCommands.Trim();

                    string[] commandsToRun = scriptCommands.Split('\n');

                    for (int i = 0; i < commandsToRun.Length; i++)
                    {
                        Shell.Run(commandsToRun[i], Console);
                    }
                }
                catch (Exception ex) { Console.WriteLine(ex.Message, SVGAIIColor.Red); }
            }
        }
    }
}
