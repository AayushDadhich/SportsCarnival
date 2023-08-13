using System;
using ModuleOne;
using Newtonsoft.Json;
using System.Threading;
using System.Net.Sockets;
using CommunicationProtocol;
using log4net;

namespace WorkshopServer
{
    class ClientHandler
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(ClientHandler));
        string clientNumber;
        TcpClient clientSocket;
        NetworkStream stream ;
        Server _server = new Server();
        RequestHandler _requestHandler;
        public ClientHandler(NetworkStream stream)
        {
            this.stream = stream;
            _requestHandler = new RequestHandler(stream);
        }
        
        public void StartClientInteraction(TcpClient clientSocket, string clientNumber)
        {
            _logger.Info("Client number " + clientNumber + " connected with IP : " + clientSocket.Client.LocalEndPoint);
            this.clientNumber = clientNumber;
            this.clientSocket = clientSocket;
            this.stream = clientSocket.GetStream();
            Thread ctThread = new Thread(HandleRequests);
            ctThread.Start();
        }

        private ISCRequest GetISCRequest()
        {
            byte[] iscRequestBytes = new byte[ApplicationConstants.bytesize];
            stream.Read(iscRequestBytes, 0, ApplicationConstants.bytesize);
            return JSONSerializer.DeSerializeBytesToISCRequest(iscRequestBytes);
        }

        private void HandleRequests()
        {
            while (true)
            {
                ISCRequest iscRequest = GetISCRequest();
                _logger.Info("Client number" + clientNumber + " request : " + clientSocket);
                if (iscRequest.data != null && !iscRequest.IsValidDataSize(iscRequest))
                {
                    throw new Exception(ApplicationConstants.InvalidDataReceived);
                };
                RequestHandler.HandleRequest(iscRequest);
            }
        }
    }
}
