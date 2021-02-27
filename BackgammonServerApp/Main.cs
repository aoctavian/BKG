using Backgammon;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace BkgSvApp
{
    class Main
    {
        private TcpListener Server { get; }
        private TcpClient Client { get; set; }

        public Main()
        {
            string ip = null;
            IPAddress[] localIP = Dns.GetHostAddresses(Dns.GetHostName()); //get own IP
            foreach (IPAddress address in localIP)
            {
                if (address.AddressFamily == AddressFamily.InterNetwork)
                {
                    ip = address.ToString();
                }
            }

            Server = new TcpListener(IPAddress.Parse(ip), DEF.PORT_SERVER_INTERNAL_SV_APP);
            Server.Start();

            Console.WriteLine("-- SERVER STARTED --");
            Console.WriteLine($"LISTENING PORT: {DEF.PORT_SERVER_INTERNAL_SV_APP}");

            using (StreamReader sr = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "GameAppVersion.txt"))
            {
                string line = sr.ReadToEnd();
                PLAYER.GameAppVersion = line;
            }

            StartServer();
        }

        private void StartServer()
        {
            Client = Server.AcceptTcpClient();
            new PLAYER(Client);
            StartServer();
        }
    }
}
