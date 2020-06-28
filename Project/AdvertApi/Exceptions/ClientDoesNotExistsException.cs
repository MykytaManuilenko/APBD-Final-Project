using System;

namespace Project_AdvertApi.Exceptions
{
    public class ClientDoesNotExistsException : Exception
    {
        public ClientDoesNotExistsException(string message) : base(message)
        {
        }

        public ClientDoesNotExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ClientDoesNotExistsException()
        {
        }
    }
}
