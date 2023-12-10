using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using DNCCorporate.Public.Web.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;

namespace DNCCorporate.Public.Web.ViewComponents;

public class LanguageSelectorViewComponent : ViewComponent
{
    private readonly RequestLocalizationOptions _localizationOptions;

    public LanguageSelectorViewComponent(IOptions<RequestLocalizationOptions> localizationOptions)
    {
        _localizationOptions = localizationOptions?.Value ?? throw new ArgumentNullException(nameof(localizationOptions));
    }

    public IViewComponentResult Invoke()
    {
        var requestCulture = CultureInfo.CurrentCulture;

        var supportedCultures = _localizationOptions.SupportedUICultures
            .Select(c => new SelectListItem
            {
                Value = c.Name,
                Text = c.DisplayName
            }).ToList();

        var routeData = new Dictionary<string, string>();

        foreach (var r in ViewContext.RouteData.Values)
        {
            routeData.Add(r.Key, r.Value.ToString());
        }

        foreach (var qs in HttpContext.Request.Query)
        {
            routeData.Add(qs.Key, qs.Value);
        }

        var result = new LanguageSelectorViewModel(requestCulture, supportedCultures, routeData);
        
        return View(result);
    }
}
