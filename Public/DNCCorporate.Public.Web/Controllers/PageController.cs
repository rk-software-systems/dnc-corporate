using DNCCorporate.Server.Contract.Content;
using Microsoft.AspNetCore.Mvc;

namespace DNCCorporate.Public.Web.Controllers
{
    /// <summary>
    /// This controller is used to render page
    /// </summary>
    public class PageController : Controller
    {
        private readonly IPageService _pageService;


        /// <summary>
        /// Initializes a new instance of the <see cref="PageController"/> class.
        /// </summary>
        public PageController(IPageService pageService)
        {
            _pageService = pageService;
        }

        public IActionResult Index(string pageurl)
        {
            var page = _pageService.GetPage(pageurl);

            if (page == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(page.ViewName))
            {
                return View(page.ViewName, page);
            }

            return View(page);
        }
    }
}
