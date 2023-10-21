/* This code is licensed under the ekzFreeUse license
 * If a license wasn't included with the program,
 * refer to https://github.com/9xbt/SVGAIITerminal/blob/main/LICENSE.md */

using System;
using Sys = Cosmos.System;
using Cosmos.System.FileSystem;
using PrismAPI.Hardware.GPU;
using System.Resources;
using OpenNIX;
using System.IO;
using OpenNIX.GUI;
using System.Reflection;

namespace OpenNIX_2
{
    public class Kernel : Sys.Kernel
    {
        public static ushort Width = 1024, Height = 768;
        public static Display Screen = Display.GetDisplay(Width, Height);
        public static SVGAIITerminal Console = new SVGAIITerminal(Width, Height, Resources.Font, FallbackTerminalUpdate);
        public static CosmosVFS FS = new CosmosVFS();

        public const string Version = "2.0";
        public static string Build = Resources.BuildDate;
        public const string Copyright = "Copyright (c) 2023 Callux Industries. All rights reserved.";
        public const string Username = "root";
        public const string Hostname = "OpenNIX";
        public static string BootTime = Time.MonthString() + "/" + Time.DayString() + "/" + Time.YearString() + ", " + Time.TimeString(true, true, true);

        protected override void BeforeRun()
        {
            Init.Boot();
            WindowManager.stopped = "NA";
        }

        protected override void Run()
        {
            Console.Write($"[{Username}@{Hostname} {DiskManager.GetUnixLikePath(Directory.GetCurrentDirectory())}]# ", SVGAIIColor.Gray);
            var input = Console.ReadLine();
            Shell.Run(input, Console);
        }

        private static void FallbackTerminalUpdate()
        {
            Screen.DrawImage(0, 0, Console.Contents, false);
            Screen.Update();
        }
    }
}
