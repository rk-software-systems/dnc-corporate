using Microsoft.AspNetCore.Mvc;

namespace DNCCorporate.Public.Web.ViewComponents
{
    public class OurServicesViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
