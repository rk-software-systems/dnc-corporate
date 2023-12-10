using Microsoft.AspNetCore.Mvc;

namespace DNCCorporate.Public.Web.ViewComponents;

public class ContactUsViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}
