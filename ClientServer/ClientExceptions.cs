using System;

namespace ClientServer
{
    public class ClientExceptions : Exception
    {
        public ClientExceptions(string message)
            : base(message) { }
    }

    public class UnevenPlayersException : ClientExceptions
    {
        public UnevenPlayersException(string message)
            : base(message) { }
    }

    public class ConnectionNotEstablishedException : ClientExceptions
    {
        public ConnectionNotEstablishedException(string message)
            : base(message) { }
    }   
}
