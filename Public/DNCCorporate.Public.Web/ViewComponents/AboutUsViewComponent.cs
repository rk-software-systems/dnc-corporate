using Microsoft.AspNetCore.Mvc;

namespace DNCCorporate.Public.Web.ViewComponents;

public class AboutUsViewComponent: ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}
