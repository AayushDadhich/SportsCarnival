using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using CommunicationProtocol;
using System.Configuration;
namespace ClientServer
{
    public class ClientServer
    {
        static System.Net.Sockets.TcpClient socket;
        static NetworkStream stream;
        static string localIPAddress = ConfigurationManager.AppSettings.Get("workshopServerIP");
        static int workshopServerPort = Convert.ToInt32(ConfigurationManager.AppSettings.Get("workshopServerPort"));

        public ClientServer()
        {
            try
            {   
                socket = new System.Net.Sockets.TcpClient(ApplicationConstants.HostName, ApplicationConstants.Portnumber);
                stream = socket.GetStream();
            }
            catch (Exception generalException)
            {
                throw new ConnectionNotEstablishedException(ApplicationConstants.ConnectionErrorMessage);
            }
           
        }

        public ISCRequest GenerateCreateTeamsISCRequest(string actionType, string gameDetailsJSON, string outputFilePath)
        {
            ISCRequest createTeamsISCRequest = new ISCRequest();
            byte[] gameDetailsJSONBytes = JSONSerializer.SerializeObjectToBytes(gameDetailsJSON);
            createTeamsISCRequest.SetIPInformation(localIPAddress, workshopServerPort, localIPAddress, workshopServerPort);
            createTeamsISCRequest.SetDataSize(gameDetailsJSONBytes.Length);
            createTeamsISCRequest.SetData(gameDetailsJSONBytes);
            createTeamsISCRequest.SetActionType(actionType);
            createTeamsISCRequest.SetOutputFilePath(outputFilePath);
            return createTeamsISCRequest;
        }

        public ISCRequest GenerateGetTeamsISCRequest(string actionType, int gameTypeId)
        {
            ISCRequest createTeamsISCRequest = new ISCRequest();
            createTeamsISCRequest.SetIPInformation(localIPAddress, workshopServerPort, localIPAddress, workshopServerPort);
            createTeamsISCRequest.SetActionType(actionType);
            createTeamsISCRequest.setValue("gameTypeId", gameTypeId.ToString());
            return createTeamsISCRequest;
        }

        public void SendISCRequest(byte[] iscRequest)
        {
            stream.Write(iscRequest, 0, iscRequest.Length);
        }

        public ICSResponse GetCreatedTeamsResponse()
        {
            ISCRequest iscRequest = new ISCRequest();
            byte[] serverResponse = new byte[ApplicationConstants.Bytesize];
            stream.Read(serverResponse, 0, serverResponse.Length);
            return JSONSerializer.DeSerializeBytesToICSResponse(serverResponse);
        }
    }
}
