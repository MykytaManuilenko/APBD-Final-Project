using System;

namespace AdvertApi.Exceptions
{
    public class NotEnougBuildingsInTheDatabaseException : Exception
    {
        public NotEnougBuildingsInTheDatabaseException(string message) : base(message)
        {
        }

        public NotEnougBuildingsInTheDatabaseException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public NotEnougBuildingsInTheDatabaseException()
        {
        }
    }
}
