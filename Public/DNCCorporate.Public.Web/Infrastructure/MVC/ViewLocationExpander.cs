using DNCCorporate.Server.Contract;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;

namespace DNCCorporate.Public.Web.Infrastructure.MVC
{
    /// <summary>
    /// This class is used to override default view search.
    /// </summary>
    public class ViewLocationExpander : IViewLocationExpander
    {

        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context,
            IEnumerable<string> viewLocations)
        {
            var workContext = context.ActionContext.HttpContext.RequestServices.GetService<IWorkContext>();
            string theme = workContext.CurrentTheme;
            string[] locations = new string[]
            {
                "/Themes/" + theme + "/Shared/{0}.cshtml",
                "/Themes/" + theme + "/{2}/{1}/{0}.cshtml",
                "/Themes/Default/{2}/{1}/{0}.cshtml"
            };

            return locations.Union(viewLocations);
        }

        public void PopulateValues(ViewLocationExpanderContext context)
        {
            context.Values["themeviewlocations"] = nameof(ViewLocationExpander);
        }
    }
}
