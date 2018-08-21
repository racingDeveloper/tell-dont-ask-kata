using System;
using System.Runtime.Serialization;

namespace TellDontAskKata.UseCase
{
    public class RejectedOrderCannotBeApprovedException : Exception
    {
        public RejectedOrderCannotBeApprovedException()
        {
        }

        public RejectedOrderCannotBeApprovedException(string message) : base(message)
        {
        }

        public RejectedOrderCannotBeApprovedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RejectedOrderCannotBeApprovedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
