namespace DNCCorporate.Public.Web.ErrorHandlings;

public class SettingsNotFoundException : Exception
{
    public SettingsNotFoundException(string message) : base(message)
    {
    }

    public SettingsNotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public SettingsNotFoundException()
    {
    }
}
