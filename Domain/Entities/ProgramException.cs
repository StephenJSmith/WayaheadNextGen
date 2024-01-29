using System.Runtime.Serialization;

namespace Domain.Entities;

[Serializable]
public class ProgramException : Exception
{
    public ProgramException()
    {
    }

    public ProgramException(string message) : base(message)
    {
    }

    public ProgramException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected ProgramException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}