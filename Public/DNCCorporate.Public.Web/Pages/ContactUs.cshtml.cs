using DNCCorporate.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DNCCorporate.Public.Web.Pages
{
    public class ContactUsModel : PageModel
    {
        public ContactUsFormRequestViewModel Form { get; set; } = new ContactUsFormRequestViewModel(string.Empty, string.Empty, string.Empty, string.Empty);

        public bool? IsSuccess { get; set; }


        public void OnGet()
        {
        }

        public void OnPost(ContactUsRequestViewModel request)
        {
            if (!ModelState.IsValid)
            {
                IsSuccess = false;
                return;
            }

            IsSuccess = true;
        }
    }
}
