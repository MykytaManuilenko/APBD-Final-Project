using System;

namespace AdvertApi.Exceptions
{
    public class WrongDateException : Exception
    {
        public WrongDateException(string message) : base(message)
        {
        }

        public WrongDateException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public WrongDateException()
        {
        }
    }
}
