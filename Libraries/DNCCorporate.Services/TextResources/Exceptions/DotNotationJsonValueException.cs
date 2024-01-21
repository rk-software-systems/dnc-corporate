namespace DNCCorporate.Services;
public class DotNotationJsonValueException : Exception
{
    public DotNotationJsonValueException() : base() { }

    public DotNotationJsonValueException(string message) : base(message)
    {
    }

    public DotNotationJsonValueException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
