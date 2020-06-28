using System;

namespace AdvertApi.Exceptions
{
    public class BuildingsOnDifferentStreetsException : Exception
    {
        public BuildingsOnDifferentStreetsException(string message) : base(message)
        {
        }

        public BuildingsOnDifferentStreetsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public BuildingsOnDifferentStreetsException()
        {
        }
    }
}
