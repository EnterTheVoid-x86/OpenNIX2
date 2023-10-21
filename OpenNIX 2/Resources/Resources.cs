/* This code is licensed under the ekzFreeUse license
 * If a license wasn't included with the program,
 * refer to https://github.com/9xbt/SVGAIITerminal/blob/main/LICENSE.md */

#pragma warning disable CS0649

using IL2CPU.API.Attribs;
using PrismAPI.Graphics;
using PrismAPI.Graphics.Fonts;
using System.Collections;
using System.IO;

namespace OpenNIX
{
    public static class Resources
    {
        [ManifestResourceStream(ResourceName = "OpenNIX_2.Resources.DefaultFont.btf")] public static byte[] rawFont;
        public static Font Font = new Font(rawFont, 16);

        [ManifestResourceStream(ResourceName = "OpenNIX_2.Resources.Logo.bmp")] private static byte[] rawLogo;
        public static Canvas Logo = Image.FromBitmap(rawLogo, false);

        [ManifestResourceStream(ResourceName = "OpenNIX_2.Resources.Wallpaper.bmp")] private static byte[] rawWallpaper;
        public static Canvas Wallpaper = Image.FromBitmap(rawWallpaper, false);

        [ManifestResourceStream(ResourceName = "OpenNIX_2.Resources.BuildDate.txt")] private static byte[] rawBuildDate;
        public static string BuildDate = System.Text.Encoding.ASCII.GetString(rawBuildDate);

        [ManifestResourceStream(ResourceName = "OpenNIX_2.Resources.Mouse.bmp")] private static byte[] rawMouse;
        public static Canvas Mouse;

        [ManifestResourceStream(ResourceName = "OpenNIX_2.Resources.MouseText.bmp")] private static byte[] rawMouseText;
        public static Canvas MouseText;

        [ManifestResourceStream(ResourceName = "OpenNIX_2.Resources.MouseDrag.bmp")] private static byte[] rawMouseDrag;
        public static Canvas MouseDrag;

        [ManifestResourceStream(ResourceName = "OpenNIX_2.Resources.Busy.bmp")] private static byte[] rawBusy;
        public static Canvas Busy;

        [ManifestResourceStream(ResourceName = "OpenNIX_2.Resources.Link.bmp")] private static byte[] rawLink;
        public static Canvas Link;

        [ManifestResourceStream(ResourceName = "OpenNIX_2.Resources.Error.bmp")] private static byte[] rawError;
        public static Canvas Error;
        public static void GenerateFont() => Font = new Font(rawFont, 16);

        public static void Initialize()
        {
            Mouse = Image.FromBitmap(rawMouse, false);
            MouseText = Image.FromBitmap(rawMouseText, false);
            MouseDrag = Image.FromBitmap(rawMouseDrag, false);
            Busy = Image.FromBitmap(rawBusy, false);
            Link = Image.FromBitmap(rawLink, false);
            Error = Image.FromBitmap(rawError, false);
            Logger.SuccessLog("Embedded resources initialized.");
        }
    }
}