using System;
using System.Runtime.Serialization;

namespace Pokladna.Exceptions
{
    [Serializable]
    internal class EmptyDatasetException : Exception
    {
        public EmptyDatasetException()
        {
        }

        public EmptyDatasetException(string message) : base(message)
        {
        }

        public EmptyDatasetException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EmptyDatasetException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}