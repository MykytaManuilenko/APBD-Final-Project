using System;

namespace AdvertApi.Exceptions
{
    public class BuildingDoesNotExistsException : Exception
    {
        public BuildingDoesNotExistsException(string message) : base(message)
        {
        }

        public BuildingDoesNotExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public BuildingDoesNotExistsException()
        {
        }
    }
}
