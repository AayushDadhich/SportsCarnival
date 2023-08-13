using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CommunicationProtocol
{
    public class ICSResponse: CommunicationProtocol
    {
        public Dictionary<string, string> headerParameters = new Dictionary<string, string>();
        public static string STATUS = "status";
        public static string ERROR_CODE = "error-code";
        public static string ERROR_MESSAGE = "error-message";
        public static string SUCCESS_MESSAGE = "success-message";

        public ICSResponse()
        {
            base.protocolType = "RESPONSE";
            base.headers = (headerParameters);
        }

        public void setData(byte[] data)
        {
            base.data = data;
        }
        public void setStatus(string status)
        {
            headerParameters.Add(STATUS, status);
        }

        public string getStatus()
        {
            return getValue(STATUS);
        }

        public string getErrorMessage()
        {
            return getValue(ERROR_MESSAGE);
        }

        public void setErrorMessage(string message)
        {
            setValue(ERROR_MESSAGE, message);
        }

        public void setSuccessMessage(string message)
        {
            setValue(SUCCESS_MESSAGE, message);
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
