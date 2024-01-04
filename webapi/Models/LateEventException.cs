using System.Runtime.Serialization;

namespace CallCenterApi.Models;

[Serializable]
public class LateEventException : Exception
{
    public LateEventException()
        : base("Timestamp is more than an hour old.")
    { }

    protected LateEventException(SerializationInfo  info, StreamingContext context) : base(info, context) { }
}