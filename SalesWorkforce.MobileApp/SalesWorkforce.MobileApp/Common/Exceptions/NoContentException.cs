using System;
using System.Runtime.Serialization;

namespace SalesWorkforce.MobileApp.Common.Exceptions
{
    [Serializable]
    public class NoContentException : Exception
    {
        public NoContentException()
        {
        }

        public NoContentException(string message) : base(message)
        {
        }

        public NoContentException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoContentException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
