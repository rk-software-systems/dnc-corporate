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

public class ContactUsModel(IEmailSenderService emailSenderService, IOptions<BusinessSettings> businessSettingsOptions, IMetaTagService metaTagService) : PageModel
{
    #region fields       

    private readonly IEmailSenderService _emailSenderService = emailSenderService;
    private readonly BusinessSettings _businessSettings = businessSettingsOptions.Value;
    #endregion

    #region properties

    public ContactUsFormRequestViewModel Form { get; set; } = new ContactUsFormRequestViewModel(string.Empty, string.Empty, string.Empty, string.Empty);

    public const string PageName = "contactus";

    #endregion


    #region methods 

    public void OnGet()
    {
        metaTagService.SetPageMetaTags(PageName);
    }

    public async Task<IActionResult> OnPost(ContactUsRequestViewModel request)
    {
        var isSuccess = ModelState.IsValid;

        if (ModelState.IsValid)
        {
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
            catch (Exception)
            {
                isSuccess = false;
            }
        }

        var result = new ContactUsResponseViewModel(isSuccess);
        return new JsonResult(result);
    }
    #endregion
}
