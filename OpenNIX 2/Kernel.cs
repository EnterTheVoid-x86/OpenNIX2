/* This code is licensed under the ekzFreeUse license
 * If a license wasn't included with the program,
 * refer to https://github.com/EnterTheVoid-x86/OpenNIX2/blob/master/LICENSE.md */

using System;
using Sys = Cosmos.System;
using Cosmos.System.FileSystem;
using PrismAPI.Hardware.GPU;
using System.Resources;
using OpenNIX;
using System.IO;
using OpenNIX.GUI;
using System.Reflection;
using System.Net.Http.Headers;

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
        public static string Username = "root";
        public static string Hostname = "OpenNIX";
        public static string BootTime = Time.MonthString() + "/" + Time.DayString() + "/" + Time.YearString() + ", " + Time.TimeString(true, true, true);

        protected override void BeforeRun()
        {
            WindowManager.stopped = "NA";
            Init.Boot();
        }

        protected override void Run()
        {
            LoginManager.Run(Console);
        }

        private static void FallbackTerminalUpdate()
        {
            Screen.DrawImage(0, 0, Console.Contents, false);
            Screen.Update();
        }
    }
}
