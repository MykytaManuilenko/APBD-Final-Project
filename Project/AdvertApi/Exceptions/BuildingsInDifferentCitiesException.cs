using System;

namespace AdvertApi.Exceptions
{
    public class BuildingsInDifferentCitiesException : Exception
    {
        public BuildingsInDifferentCitiesException(string message) : base(message)
        {
        }

        public BuildingsInDifferentCitiesException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public BuildingsInDifferentCitiesException()
        {
        }
    }
}
