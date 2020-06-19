using System;
using System.Runtime.Serialization;

namespace AdvertApi.Services
{
    [Serializable]
    internal class BuildingAreNotNextToEachOtherException : Exception
    {
        public BuildingAreNotNextToEachOtherException()
        {
        }

        public BuildingAreNotNextToEachOtherException(string message) : base(message)
        {
        }

        public BuildingAreNotNextToEachOtherException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BuildingAreNotNextToEachOtherException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}