using System;
using System.Runtime.Serialization;

namespace Pokladna.Exceptions
{
    [Serializable]
    internal class CouponValidationException : Exception
    {
        public CouponValidationException()
        {
        }

        public CouponValidationException(string message) : base(message)
        {
        }

        public CouponValidationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CouponValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}