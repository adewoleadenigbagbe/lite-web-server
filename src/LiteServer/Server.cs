using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Runtime.CompilerServices;

namespace LiteServer
{
    public class Server
    {
        private readonly HttpListener _listener = new HttpListener();
        private const int _port = 3022;
        public void Start()
        {
            _listener.Prefixes.Add("http://localhost" +":" +_port +"/");

            var hostIps = GetHostIps();
            foreach (var ip in hostIps)
            {
                _listener.Prefixes.Add("http://" + ip + ":" + _port + "/");
            }

            if (_listener.IsListening)
            {
                throw new InvalidOperationException("Server is currently running");
            }

            _listener.Start();

            Console.WriteLine("Server is currently listening in port:" + _port);

            //_listener.Abort();
        }

        private static IEnumerable<string> GetHostIps()
        {
            var hostName = Dns.GetHostName();
            var ipAddresses =  Dns.GetHostEntry(hostName).AddressList;

            foreach (var ipAddress in ipAddresses.Where(ip => ip.AddressFamily == AddressFamily.InterNetwork))
            {
                yield return ipAddress.ToString();
            }
        }
    }
}
