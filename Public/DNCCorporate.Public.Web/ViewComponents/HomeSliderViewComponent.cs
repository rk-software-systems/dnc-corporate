using Microsoft.AspNetCore.Mvc;

namespace DNCCorporate.Public.Web.ViewComponents;

public class HomeSliderViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}
