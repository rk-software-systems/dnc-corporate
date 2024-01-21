using System.Globalization;
using DNCCorporate.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;

namespace DNCCorporate.Public.Web.Framework.Localizations;

public static class LocalizationExtension
{
    /// <summary>
    /// localize request according to {culture} route value.
    /// define supported cultures list, 
    /// define default culture for a fallback
    /// </summary>
    /// <param name="services"></param>
    /// <param name="settings"></param>
    public static void ConfigureRequestLocalization(this IServiceCollection services, LocalizationSettings settings)
    {

        ArgumentNullException.ThrowIfNull(settings, nameof(settings));

        var cultures = settings.AvailableCultures
            .Select(x => new CultureInfo(x));

        services.Configure<RequestLocalizationOptions>(ops =>
        {
            ops.DefaultRequestCulture = new RequestCulture(settings.DefaultCulture);
            ops.SupportedCultures = cultures.OrderBy(x => x.EnglishName).ToList();
            ops.SupportedUICultures = cultures.OrderBy(x => x.EnglishName).ToList();

            // add RouteValueRequestCultureProvider to the beginning of the providers list. 
            ops.RequestCultureProviders.Insert(0, new RouteValueRequestCultureProvider(settings));
        });
    }
}
