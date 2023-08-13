using Newtonsoft.Json;
using System.Collections.Generic;

namespace CommunicationProtocol
{
    [JsonObject(MemberSerialization.OptOut)]
    public class CommunicationProtocol
    {

        public int size;
        public string REQUEST = "request";
        public string RESPONSE = "response";
        public byte[] data;
        public string protocolVersion;
        public string protocolFormat;
        public string protocolType;
        public string sourceIp;
        public int sourcePort;
        public string destIp;
        public int destPort;
        public Dictionary<string, string> headers;
    }
}
