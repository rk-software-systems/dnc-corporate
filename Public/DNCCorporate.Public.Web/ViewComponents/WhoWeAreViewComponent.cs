﻿using Microsoft.AspNetCore.Mvc;

namespace DNCCorporate.Public.Web.ViewComponents
{
    public class WhoWeAreViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}