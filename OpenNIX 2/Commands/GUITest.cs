using PrismAPI.Hardware.GPU;
using PrismAPI.Graphics;
using Cosmos.System;
using PrismAPI.UI;
using PrismAPI.UI.Controls;
using Cosmos.Core.Memory;
using OpenNIX_2;
using PrismAPI.UI.Config;
using Cosmos.Core;
using PrismAPI.Tools.Extentions;

namespace OpenNIX
{
    public partial class Commands
    {
        public static Display Canvas = null!;
        public static Window MyWindow = null!;
        public static Label MyLabel = null!;
        public static Button MyButton = null!;
        private static short framesToHeapCollect = 5;
        public static int LastMouseX = (int)MouseManager.X, LastMouseY = (int)MouseManager.Y, MouseOffsetX = 0, MouseOffsetY = 0;
        public static Drawable Taskbar = null!;
        public static Label FPSWidget = null!;
        public static void GUITest()
        {
            Canvas = Display.GetDisplay(800, 600); // Define the canvas instance.

            MouseManager.ScreenWidth = 800;
            MouseManager.ScreenHeight = 600;

            MyWindow = new(50, 50, 500, 400, "My Window");
            MyLabel = new(15, 15, $"{Canvas.GetFPS()} FPS");
            MyButton = new(50, 50, 128, 64, 4, "Exit GUI Mode", ThemeStyle.Holo);

            FPSWidget = new(15, 15, "Initializing...");
            Taskbar = new(0, Canvas.Height - 48, Canvas.Width, 48);

            MyWindow.Controls.Add(MyLabel);
            WindowManager.Windows.Add(MyWindow);
            MyWindow.Controls.Add(MyButton);

            WindowManager.Widgets.Add(FPSWidget);

            for (; ; )
            {
                Run();
            }
        }

        public static void Run()
        {
            Canvas.DrawImage(0, 0, Resources.Wallpaper, false); // Draw background.
            FPSWidget.Contents = $"{Canvas.GetFPS()} FPS\n{Canvas.GetName()}\n{StringEx.GetMegaBytes(GCImplementation.GetUsedRAM())} MB";
            Taskbar.Clear(Color.DeepGray);
            Taskbar.DrawString(0, 28, $"{WindowManager.Windows.Count} window{((WindowManager.Windows.Count == 1) ? " is open." : "s are open.")}", default, Color.White);
            WindowManager.Update(Canvas);
            Canvas.DrawImage((int)MouseManager.X - MouseOffsetX, (int)MouseManager.Y - MouseOffsetY, Resources.Mouse, true); // Draw the mouse.
            framesToHeapCollect--;
            if (framesToHeapCollect <= 0)
            {
                Heap.Collect();
                framesToHeapCollect = 5;
            }
            Canvas.Update();
        }
    }
}
