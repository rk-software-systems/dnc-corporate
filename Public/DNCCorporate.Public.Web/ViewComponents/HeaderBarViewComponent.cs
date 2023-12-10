using Microsoft.AspNetCore.Mvc;

namespace DNCCorporate.Public.Web.ViewComponents;

public class HeaderBarViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}
