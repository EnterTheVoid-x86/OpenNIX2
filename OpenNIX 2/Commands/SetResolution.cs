using OpenNIX_2;
using PrismAPI.Hardware.GPU;
using System.ComponentModel.Design;

namespace OpenNIX
{
    public static partial class Commands
    {
        public static void SetResolution()
        {
            if (GUI.WindowManager.stopped == "true" || GUI.WindowManager.stopped == "NA" || GUI.WindowManager.stopped == "resolution changed")
            {
                Console.Write("Choose new resolution (example: 1024x768): ");
                var input = Console.ReadLine();
                input = input.Trim();

                string[] args = input.Split('x');

                Kernel.Screen = Display.GetDisplay(ushort.Parse(args[0]), ushort.Parse(args[1]));
                GUI.WindowManager.stopped = "resolution changed";
            }
            else
            {
                Console.WriteLine("setres: error: setres only works on console mode");
            }    
        }
    }
}
