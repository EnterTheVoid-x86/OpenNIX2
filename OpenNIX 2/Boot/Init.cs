using OpenNIX.GUI;
using OpenNIX_2;

namespace OpenNIX
{
    public static class Init
    {
        private static SVGAIITerminal Console = Kernel.Console;
        public static void Boot()
        {
            Kernel.Screen.Update();
            Logger.InfoLog($"OpenNIX version {Kernel.Version} build {Kernel.Build}");
            Logger.InfoLog($"{Kernel.Copyright}");
            Logger.InfoLog($"CPU detected as: {Cosmos.Core.CPU.GetCPUBrandString()}");
            Logger.InfoLog("Running init process...");
            Logger.SuccessLog("Screen initalized.");
            Resources.Initialize();
            MouseDriver.Initialize();
            DiskManager.InitFS(Kernel.FS);
            NetworkManager.Initialize();
            Logger.InfoLog("Dropping into Shell...");
        }
    }
}