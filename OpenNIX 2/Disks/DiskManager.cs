﻿using System.IO;
using Cosmos.System.FileSystem;
using Cosmos.System.FileSystem.VFS;
using OpenNIX;
using OpenNIX_2;

namespace OpenNIX
{
    public static class DiskManager
    {
        private static SVGAIITerminal Console = Kernel.Console;
        static string input = "log info Font loaded.";
        public static void InitFS(CosmosVFS fs)
        {
            try
            {
                VFSManager.RegisterVFS(fs);
                Directory.GetFiles(@"0:\");
                Directory.SetCurrentDirectory(@"0:\");

                Logger.SuccessLog("Filesystem intialized.");

                if (!Directory.Exists(@"0:\System"))
                {
                    Directory.CreateDirectory(@"0:\System\");
                    Directory.CreateDirectory(@"0:\System\Fonts");
                }

            }
            catch
            {
                Logger.WarnLog("Failed to initialize filesystem! Continuing without FS support...");
            }
        }

        public static string GetCosmosLikePath(string path)
        {
            return @"0:\" + path.Substring(1).Replace("/", "\\");
        }

        public static string GetUnixLikePath(string path)
        {
            return path.Replace("\\", "/").Replace("0:/", "/");
        }
    }
}