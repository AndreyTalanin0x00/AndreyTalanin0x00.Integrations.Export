using System;

namespace AndreyTalanin0x00.Integrations.Export.Exceptions;

[Serializable]
public class ExportException : Exception
{
    public ExportException()
    {
    }

    public ExportException(string message)
        : base(message)
    {
    }

    public ExportException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
