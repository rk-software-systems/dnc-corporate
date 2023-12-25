using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Options;
using DNCCorporate.ViewModels;

namespace DNCCorporate.Services;

public class EmailSenderService(IOptions<SmtpSettings> smtpOptions) : IEmailSenderService
{

    #region fields     

    private readonly SmtpSettings _settings = smtpOptions.Value;
    #endregion

    #region methods

    public async Task SendEmail(EmailMessageViewModel message)
    {
        ArgumentNullException.ThrowIfNull(message, nameof(message));

        using var client = new SmtpClient();

        if (!string.IsNullOrEmpty(_settings.Username) && !string.IsNullOrEmpty(_settings.Password))
        {
            client.UseDefaultCredentials = false;
            var credentials = new NetworkCredential(_settings.Username, _settings.Password);
            client.Credentials = credentials;
        }

        client.Host = _settings.HostName;
        client.Port = _settings.Port;
        client.EnableSsl = _settings.UseSsl;
        using var emailMessage = new MailMessage();

        emailMessage.To.Add(message.Recipient);

        emailMessage.IsBodyHtml = true;
        emailMessage.From = new MailAddress(_settings.FromEmail);

        emailMessage.Subject = message.Subject;
        emailMessage.Body = message.MessageBody;

        await client.SendMailAsync(emailMessage);
    }

    #endregion

}
