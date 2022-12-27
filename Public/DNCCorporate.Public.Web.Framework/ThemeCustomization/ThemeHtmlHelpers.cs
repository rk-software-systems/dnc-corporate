﻿using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace DNCCorporate.Public.Web.Framework.ThemeCustomization
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
            var settings = html?.ViewContext.HttpContext.RequestServices.GetService<IOptions<ThemeSettings>>() ?? throw new ArgumentNullException(nameof(html));
            return new HtmlString($"/themes/{settings.Value.CurrentTheme}/");
        }
    }
}