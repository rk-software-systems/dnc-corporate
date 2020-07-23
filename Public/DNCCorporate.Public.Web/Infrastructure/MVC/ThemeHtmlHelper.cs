using DNCCorporate.Server.Contract;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.DependencyInjection;

namespace DNCCorporate.Public.Web.Infrastructure.MVC
{
    /// <summary>
    /// This class contains Theme related Html helper extensions
    /// </summary>
    public static class ThemeHtmlHelper
    {
        /// <summary>
        /// Get base path to working theme content
        /// </summary>
        /// <param name="html">Html helper entity <see cref="IHtmlHelper"/></param>
        /// <returns>Html string with base theme content path</returns>
        public static HtmlString ThemeContentPath(this IHtmlHelper html)
        {
            var workContent = html.ViewContext.HttpContext.RequestServices.GetService<IWorkContext>();
            return new HtmlString($"/themes/{workContent.CurrentTheme}/");
        }
    }
}
