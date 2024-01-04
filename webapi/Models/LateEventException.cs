namespace CallCenterApi.Models;

[Serializable]
public class LateEventException : Exception
{
    public LateEventException(string message)
        : base(message)
    { }
}