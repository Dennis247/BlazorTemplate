using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace BlazorTemplate.api.Helpers.Utils
{
    public class WorkStationHelper
    {
        private static string GetHostName(string ipAddress)
        {
            try
            {
                IPHostEntry entry = Dns.GetHostEntry(ipAddress);
                if (entry != null)
                {
                    return entry.HostName;
                }
            }
            catch (SocketException ex)
            {
                return null;
            }

            return null;
        }


        public static string GetUserIpAddress()
        {
            var ipaddress = Dns.GetHostAddresses(Dns.GetHostName())
                .First(a => a.AddressFamily == AddressFamily.InterNetwork).ToString();
            return ipaddress;
        }


        public static string getWSSignature()
        {
            return $"{GetUserIpAddress()}|{GetHostName(GetUserIpAddress())}";
        }

    }
}
