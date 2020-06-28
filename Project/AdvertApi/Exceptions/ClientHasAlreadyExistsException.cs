using System;

namespace AdvertApi.Exceptions
{
    public class ClientHasAlreadyExistsException : Exception
    {
        public ClientHasAlreadyExistsException(string message) : base(message)
        {
        }

        public ClientHasAlreadyExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ClientHasAlreadyExistsException()
        {
        }
    }
}
