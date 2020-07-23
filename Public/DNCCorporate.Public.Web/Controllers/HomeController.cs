using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DNCCorporate.Public.Web.Models;
using DNCCorporate.Server.Contract;

namespace DNCCorporate.Public.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWorkContext _workContext;

        public HomeController(IWorkContext workContext)
        {
            _workContext = workContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult RedirectToDefaultLanguage()
        {
            return RedirectToAction("Index", new { lang = _workContext.DefaultLanguage });
        }
    }
}
