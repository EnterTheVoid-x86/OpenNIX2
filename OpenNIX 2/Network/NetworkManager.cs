using Cosmos.System.Network;
using Cosmos.System.Network.IPv4.UDP.DHCP;
using System;

namespace OpenNIX
{
    public static class NetworkManager
    {
        public static void GetIPAddress()
        {
            using (var xClient = new DHCPClient())
            {
                /** Send a DHCP Discover packet **/
                //This will automatically set the IP config after DHCP response
                try
                {
                    xClient.SendDiscoverPacket();
                    Logger.SuccessLog("IP address configured.");
                    Logger.SuccessLog("Network initalized.");
                }
                catch (Exception e)
                {
                    Logger.WarnLog("Failed to get IP address!");
                }
            }
        }
        public static void Initialize()
        {
            GetIPAddress();
        }
    }
}