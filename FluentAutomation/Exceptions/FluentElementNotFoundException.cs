namespace FluentAutomation.Exceptions
{
    using System.Runtime.Serialization;

    public class FluentElementNotFoundException : FluentException
    {
        public FluentElementNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public FluentElementNotFoundException(string message, params object[] formatParams)
            : base(string.Format(message, formatParams))
        {
        }
    }
}