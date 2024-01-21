using DNCCorporate.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;

namespace DNCCorporate.Public.Web.Framework;

public class RouteValueRequestCultureProvider(LocalizationSettings settings) : IRequestCultureProvider
{
    private readonly LocalizationSettings _settings = settings;

    /// <summary>
    /// get {culture} route value from path string, 
    /// </summary>
    /// <param name="httpContext"></param>
    /// <returns>ProviderCultureResult depends on path {culture} route parameter, or default culture</returns>
    public Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
    {
        ArgumentNullException.ThrowIfNull(httpContext, nameof(httpContext));

        var path = httpContext.Request.Path;

        if (!path.HasValue)
        {
            return Task.FromResult(new ProviderCultureResult(_settings.DefaultCulture));
        }

        var routeValues = path.Value.Split('/', StringSplitOptions.RemoveEmptyEntries);
        if (routeValues.Length <= 0)
        {
            return Task.FromResult(new ProviderCultureResult(_settings.DefaultCulture));
        }

        if (!_settings.AvailableCultures.Any(x => x.Equals(routeValues[0], StringComparison.OrdinalIgnoreCase)))
        {
            return Task.FromResult(new ProviderCultureResult(_settings.DefaultCulture));
        }

        return Task.FromResult(new ProviderCultureResult(routeValues[0]));
    }
}
