namespace FluentAutomation.Exceptions
{
    using System.Runtime.Serialization;

    public class FluentExpectFailedException : FluentAssertFailedException
    {
        public FluentExpectFailedException(string message, params object[] formatParams)
            : base(message, formatParams)
        {
        }

        public FluentExpectFailedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }

    public class FluentAssertFailedException : FluentException
    {
        public FluentAssertFailedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public FluentAssertFailedException(string message, params object[] formatParams)
            : base(string.Format(message, formatParams))
        {
        }
    }
}