using System;
using Cosmos.HAL;
using OpenNIX_2;
using PrismAPI.Graphics;

namespace OpenNIX.GUI.Apps
{
    public class Desktop : Window
    {
        private byte lastMinute = RTC.Minute;

        public static Canvas BackgroundImage = Resources.Wallpaper;
        public static bool BackgroundChangeRequest = true;

        public Desktop() : base(0, 0, WindowManager.Screen.Width, WindowManager.Screen.Height, "WM.Desktop") { Movable = false; }

        public override void Render()
        {
            try
            {
                for (int y = 0; y < Kernel.Screen.Height; y += Resources.Wallpaper.Height)
                    for (int x = 0; x < Kernel.Screen.Width; x += Resources.Wallpaper.Width) // Tiling background image
                        Contents.DrawImage(x, y, BackgroundImage, false);

                string timeString = DateTime.Now.ToString("t");

                Contents.DrawFilledRectangle(0, 0, Contents.Width, 20, 0, Color.LightBlack);
                Contents.DrawString(2, 2, "1", Resources.Font, Color.White);
                Contents.DrawString(Contents.Width - Resources.Font.MeasureString(timeString) - 2, 2, timeString, Resources.Font, BorderColor2);

                lastMinute = RTC.Minute;

                if (!WindowManager.FocusedWindow.Name.StartsWith("WM."))
                    Contents.DrawString(18, 2, WindowManager.FocusedWindow.Name, Resources.Font, BorderColor2);
            }
            catch { }

            BackgroundChangeRequest = false;
        }

        public override void Update()
        {
            if (BackgroundChangeRequest || RTC.Minute != lastMinute || WindowManager.FocusedWindow != WindowManager.LastFocusedWindow)
                Render();
        }
    }
}
