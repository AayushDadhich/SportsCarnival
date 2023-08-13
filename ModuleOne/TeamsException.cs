using System;

namespace WorkshopServer
{
    public class TeamsException : Exception
    {
        public TeamsException(string message)
            : base(message) { }
    }

    public class UnevenPlayersException : TeamsException
    {
        public UnevenPlayersException(string message)
            : base(message) { }
    }

    public class InvalidGameTypeIdException : TeamsException
    {
        public InvalidGameTypeIdException(string message)
            : base(message) { }
    }

    public class InvalidActionTypeException : TeamsException
    {
        public InvalidActionTypeException(string message)
            : base(message) { }
    }

    public class InvalidPlayerIdException : TeamsException
    {
        public InvalidPlayerIdException(string message)
            : base(message) { }
    }
}
