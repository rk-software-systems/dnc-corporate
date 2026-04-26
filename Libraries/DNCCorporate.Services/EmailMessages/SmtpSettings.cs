namespace DNCCorporate.Services;

public class SmtpSettings
{
    public required string HostName { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public int Port { get; set; }

    public bool UseSsl { get; set; }

    public required string FromEmail { get; set; }
}
