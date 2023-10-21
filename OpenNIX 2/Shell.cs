/* This code is licensed under the ekzFreeUse license
 * If a license wasn't included with the program,
 * refer to https://github.com/EnterTheVoid-x86/OpenNIX2/blob/master/LICENSE.md */


namespace OpenNIX
{
    public static class Shell
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
                    string currentTime = Time.MonthString() + "/" + Time.DayString() + "/" + Time.YearString() + ", " + Time.TimeString(true, true, true);
                    Commands.Info(currentTime);
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

                case "onvi":
                    Commands.StartONVI(args);
                    break;

                case "license":
                    Commands.License();
                    break;

                case "script":
                    Commands.Script(input);
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
                    Commands.LS();
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

                default:
                    Console.WriteLine($"Command \"{args[0].Trim()}\" not found.", SVGAIIColor.Red);
                    break;
            }
        }
    }
}