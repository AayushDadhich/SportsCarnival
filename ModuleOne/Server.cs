using System;
using System.Net.Sockets;
using System.Net;
using log4net;
using System.Configuration;

namespace WorkshopServer
{
    class Server
    {
        static string localIPAddress = ConfigurationManager.AppSettings.Get("localIPAddress");
        static int workshopServerPort = Convert.ToInt32(ConfigurationManager.AppSettings.Get("workshopServerPort"));
        static IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Loopback, 8888);
        static TcpListener listener = new TcpListener(ipEndPoint);
        static TcpClient clientSocket = default(TcpClient);
        private static readonly ILog _logger = LogManager.GetLogger(typeof(Server));
        static int clientCounter = 0;
        public void Start()
        {
            listener.Start();
            Console.WriteLine(ApplicationConstants.ServerStartedMessage, ipEndPoint.Address, ipEndPoint.Port);
            _logger.Info("Server started");
            while (true)
            {
                clientCounter += 1;
                clientSocket = listener.AcceptTcpClient();
                Console.WriteLine("Client No:" + Convert.ToString(clientCounter) + " started!");
                ClientHandler client = new ClientHandler(clientSocket.GetStream());
                client.StartClientInteraction(clientSocket, Convert.ToString(clientCounter));
            }
        }
    }
}