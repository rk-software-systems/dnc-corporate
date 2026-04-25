using System.Globalization;
using DNCCorporate.Public.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;

namespace DNCCorporate.Public.Web.ViewComponents;

public class LanguageSelectorViewComponent(IOptions<RequestLocalizationOptions> localizationOptions) : ViewComponent
{
    private readonly RequestLocalizationOptions _localizationOptions = localizationOptions?.Value ?? throw new ArgumentNullException(nameof(localizationOptions));

    public IViewComponentResult Invoke()
    {
        var requestCulture = CultureInfo.CurrentCulture;

        var supportedCultures = _localizationOptions.SupportedUICultures?
            .Select(c => new SelectListItem
            {
                Value = c.Name,
                Text = c.DisplayName
            }).ToList() ?? [];

        var routeData = new Dictionary<string, string>();

        foreach (var r in ViewContext.RouteData.Values)
        {
            var value = r.Value?.ToString();
            if (value != null)
            {
                routeData.Add(r.Key, value);
            }
        }

        foreach (var qs in HttpContext.Request.Query)
        {
            var value = qs.Value.ToString();
            if (value != null)
            {
                routeData.Add(qs.Key, value);
            }
        }

        var result = new LanguageSelectorViewModel(requestCulture, supportedCultures, routeData);
        
        return View(result);
    }
}
