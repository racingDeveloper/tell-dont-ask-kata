namespace TellDontAskKata.UseCase
{
    using System;
    using System.Runtime.Serialization;

    public class OrderCannotBeShippedTwiceException : Exception
    {
        public OrderCannotBeShippedTwiceException()
        {
        }

        public OrderCannotBeShippedTwiceException(string message) : base(message)
        {
        }

        public OrderCannotBeShippedTwiceException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected OrderCannotBeShippedTwiceException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}