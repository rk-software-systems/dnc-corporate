using DNCCorporate.Public.Web.Infrastructure.MVC;
using DNCCorporate.Server.Contract;
using DNCCorporate.Server.Contract.Content;
using Microsoft.AspNetCore.Mvc;

namespace DNCCorporate.Public.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWorkContext _workContext;
        private readonly IPageService _pageService;

        public HomeController(IWorkContext workContext,
            IPageService pageService)
        {
            _workContext = workContext;
            _pageService = pageService;
        }

        public IActionResult RedirectToDefaultLanguage(string anyslug)
        {
            string slug = anyslug;
            if (string.IsNullOrEmpty(slug))
            {
                slug = _pageService.GetDefaultPortalPage()?.SFUrl;
            }

            return RedirectToRoute(RouteConstants.LOCALIZED_PAGE_ROUTE_NAME,
                new 
                { 
                    lang = _workContext.DefaultLanguage, 
                    pageurl = slug 
                });
        }
    }
}
