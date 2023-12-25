using DNCCorporate.ViewModels;

namespace DNCCorporate.Services;

public interface IEmailSenderService
{
    Task SendEmail(EmailMessageViewModel message);
}
