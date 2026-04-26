using DNCCorporate.Public.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace DNCCorporate.Public.Web.ViewComponents;

public class PageTitleViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(string pageName)
    {
        var model = new PageTitleViewModel(pageName);
        return View(model);
    }
}
