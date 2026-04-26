using System.Globalization;
using System.Text;
using DNCCorporate.Public.Web.Framework;
using DNCCorporate.Public.Web.Infrastructure;
using DNCCorporate.Services;
using DNCCorporate.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace DNCCorporate.Public.Web.Pages;

public class ContactUsModel(
    ILogger<ContactUsModel> logger,
    IGoogleReCaptchaService googleReCaptchaService,
    IEmailSenderService emailSenderService,
    IOptions<BusinessSettings> businessSettingsOptions, 
    IMetaTagService metaTagService) : PageModel
{
    #region fields       

    private readonly ILogger _logger = logger;
    private readonly IGoogleReCaptchaService _googleReCaptchaService = googleReCaptchaService;
    private readonly IMetaTagService _metaTagService = metaTagService;
    private readonly IEmailSenderService _emailSenderService = emailSenderService;
    private readonly BusinessSettings _businessSettings = businessSettingsOptions.Value;
    #endregion

    #region properties

    public string ReCaptchaSiteKey => _googleReCaptchaService.SiteKey;

    public ContactUsFormRequestViewModel Form { get; set; } = new ContactUsFormRequestViewModel(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);

    public const string PageName = "contactus";

    #endregion


    #region methods 

    public void OnGet()
    {
        _metaTagService.SetPageMetaTags(PageName);
    }

    public async Task<IActionResult> OnPost(ContactUsRequestViewModel request)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));

        var isSuccess = ModelState.IsValid;

        if (isSuccess)
        {
            if (string.IsNullOrEmpty(request.Form.ReCaptchaToken))
            {
                _logReCaptchaTokenIsEmptyError(_logger, null);
                isSuccess = false;
            }
            else
            {
                var isCaptchaValid = await _googleReCaptchaService.Verify(request.Form.ReCaptchaToken);
                if (!isCaptchaValid)
                {
                    _logReCaptchaVerificationWarning(_logger, null);
                    isSuccess = false;
                }
            }
        }

        if (isSuccess)
        {
#pragma warning disable CA1031 // Do not catch general exception types
            try
            {
                var sb = new StringBuilder();
                sb.AppendLine(CultureInfo.InvariantCulture, $"<p>Full Name: {request.Form.FullName}</p>");
                sb.AppendLine(CultureInfo.InvariantCulture, $"<p>Email Address: {request.Form.EmailAddress}</p>");
                sb.AppendLine(CultureInfo.InvariantCulture, $"<p>Subject: {request.Form.Subject}</p>");
                sb.AppendLine(CultureInfo.InvariantCulture, $"<p>Message: {request.Form.Message}</p>");
                sb.AppendLine(CultureInfo.InvariantCulture, $"<p>IP Address: {HttpContext.Connection.RemoteIpAddress}</p>");
                sb.AppendLine(CultureInfo.InvariantCulture, $"<p>Sent On: {DateTime.UtcNow}</p>");

                await _emailSenderService.SendEmail(new EmailMessageViewModel
                (
                    $"Contact Us Form Submission - {request.Form.FullName} - {request.Form.EmailAddress}",
                    sb.ToString(),
                    _businessSettings.Email
                ));
            }
            catch (Exception ex)
            {
                _logSendEmailError(_logger, ex);
                isSuccess = false;
            }
#pragma warning restore CA1031 // Do not catch general exception types
        }

        var result = new ContactUsResponseViewModel(isSuccess);
        return new JsonResult(result);
    }
    #endregion

    #region logging

    private static readonly Action<ILogger, Exception?> _logReCaptchaTokenIsEmptyError = LoggerMessage.Define(
       LogLevel.Error,
       10000001,
       "ReCaptcha token is empty.");

    private static readonly Action<ILogger, Exception?> _logReCaptchaVerificationWarning = LoggerMessage.Define(
       LogLevel.Warning,
       10000002,
       "ReCaptcha verification is not successful.");

    private static readonly Action<ILogger, Exception?> _logSendEmailError = LoggerMessage.Define(
       LogLevel.Error,
       10000003,
       "Error occurred while sending email.");
    #endregion
}
