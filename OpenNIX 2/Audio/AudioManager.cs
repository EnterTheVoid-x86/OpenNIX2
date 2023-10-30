using Cosmos.HAL.Drivers.Audio;
using Cosmos.System.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenNIX
{
    public static class AudioManager
    {
        public static AudioDriver driver;
        public static AudioMixer mixer;
        public static bool AudioEnabled = false;
        public static void Init()
        {
            try
            {
                driver = AC97.Initialize(4096);
                mixer = new AudioMixer();
                AudioEnabled = true;
                Logger.SuccessLog("Audio manager initalized.");
            }
            catch (InvalidOperationException)
            {
                Logger.WarnLog("No AC97 device found!");
            }
            catch (Exception EX)
            {
                Logger.WarnLog($"Error: {EX.Message}");
            }
        }
    }
}
