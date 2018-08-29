using System;
using System.Runtime.Serialization;

namespace TellDontAskKata.UseCase
{
    public class UnknownProductException : Exception
    {
        public UnknownProductException()
        {
        }

        public UnknownProductException(string message) : base(message)
        {
        }

        public UnknownProductException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UnknownProductException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
