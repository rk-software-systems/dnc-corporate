using DNCCorporate.Services;
using DNCCorporate.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DNCCorporate.Public.Web.Pages
{
    public class ContactUsModel(IEmailSenderService emailSenderService) : PageModel
    {
        private readonly IEmailSenderService _emailSenderService = emailSenderService;

        public ContactUsFormRequestViewModel Form { get; set; } = new ContactUsFormRequestViewModel(string.Empty, string.Empty, string.Empty, string.Empty);

        public bool? IsSuccess { get; set; }


        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(ContactUsRequestViewModel request)
        {
            IsSuccess = ModelState.IsValid;            

            if (ModelState.IsValid)
            {
                try
                {
                    await _emailSenderService.SendEmail(new EmailMessageViewModel
                    {
                        To = ""
                    })
                }
                catch (System.Exception)
                {
                    IsSuccess = false;
                }
            }

            return new PartialViewResult
            {
                ViewName = "_ContactUsResultPartial",
                ViewData = ViewData,
                TempData = TempData
            };
        }
    }
}
