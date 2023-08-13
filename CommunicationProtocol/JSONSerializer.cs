using Newtonsoft.Json;

namespace CommunicationProtocol
{
    public class JSONSerializer
    {
        public static byte[] SerializeObjectToBytes(object objectToSerialize)
        {
            string serializedObjectString = JsonConvert.SerializeObject(objectToSerialize);
            return System.Text.Encoding.Unicode.GetBytes(serializedObjectString);
        }

        public static ICSResponse DeSerializeBytesToICSResponse(byte[] bytesToDeserialize)
        {
            string responseJSON = System.Text.Encoding.Unicode.GetString(bytesToDeserialize);
            return JsonConvert.DeserializeObject<ICSResponse>(responseJSON);
        }

        public static ISCRequest DeSerializeBytesToISCRequest(byte[] bytesToDeserialize)
        {
            string responseJSON = System.Text.Encoding.Unicode.GetString(bytesToDeserialize);
            return JsonConvert.DeserializeObject<ISCRequest>(responseJSON);
        }
    }
}
