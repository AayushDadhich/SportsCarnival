using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CommunicationProtocol
{
    [JsonObject(MemberSerialization.OptOut)]
    public class ISCRequest: CommunicationProtocol
    {
        public Dictionary<string, string> headerParameters = new Dictionary<string, string>();
        public string ACTION_TYPE = "action-type";
        public ISCRequest()
        {
            base.protocolType = "REQUEST";
            base.headers = (headerParameters);
        }

        public void SetData(byte[] data)
        {
            base.data = data;
        }

        public void SetDataSize(int dataSize)
        {
            base.size = dataSize;
        }

        public bool IsValidDataSize(ISCRequest request)
        {
            return request.size == request.data.Length ? true : false;
        }

        public void SetIPInformation(string sourceIP, int sourcePort, string destinationIP, int destinationPort)
        {
            base.sourceIp = sourceIP;
            base.sourcePort = sourcePort;
            base.destIp = destinationIP;
            base.destPort = destinationPort;
        }

        public void SetActionType(string actionType)
        {
            headerParameters.Add(ACTION_TYPE, actionType);
        }

        public void SetOutputFilePath(string outputFilePath)
        {
            headerParameters.Add("outputFilePath", outputFilePath);
        }

        public string GetOutputFilePath()
        {
            string outputFilePath;
            headerParameters.TryGetValue("outputFilePath", out outputFilePath);
            return outputFilePath;
        }

        public byte[] getRequestData()
        {
            return data;
        }

        public string getActionType()
        {
            return getValue(ACTION_TYPE);
        }

        public String getValue(string key)
        {
            Dictionary<string, string> headers = base.headers;
            return headers[key];
        }

        public void setValue(string key, string value)
        {
            headerParameters.Add(key, value);
            headers = (headerParameters);
        }
    }
}
