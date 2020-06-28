using System;

namespace AdvertApi.Exceptions
{
    public class RefreshTokenDoesNotExistsException : Exception
    {
        public RefreshTokenDoesNotExistsException(string message) : base(message)
        {
        }

        public RefreshTokenDoesNotExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public RefreshTokenDoesNotExistsException()
        {
        }
    }
}
