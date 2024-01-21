using DNCCorporate.Contracts;
using DNCCorporate.Public.Web.Framework;
using DNCCorporate.Services;
using Microsoft.Extensions.Localization;

namespace DNCCorporate.Public.Web.Infrastructure;

/// <summary>
/// This class contains service collection registration method that is used to register Service implementations and dependencies.
/// </summary>
public static class PublicWebRegistrationExtensions
{
    /// <summary>
    /// Add application specific services to service collection
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/></param>
    /// <param name="configuration"><see cref="IConfiguration"/></param>
    public static void RegisterDNCServices(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration, nameof(configuration));

        services.AddScoped<IWorkContext, WebWorkContext>();

        services.Configure<BusinessSettings>(configuration.GetSection(nameof(BusinessSettings)));

        // theme and text resources
        services.Configure<LocalizationSettings>(configuration.GetSection(nameof(LocalizationSettings)));
        services.Configure<ThemeSettings>(configuration.GetSection(nameof(ThemeSettings)));
        services.AddSingleton<ITextResourceQueryService, TextResourceQueryService>();
        services.AddSingleton<IStringLocalizerFactory, JsonStringLocalizerFactory>();
        services.AddSingleton<TextResourceCultureLocalizer>();

        // emails
        services.Configure<SmtpSettings>(configuration.GetSection(nameof(SmtpSettings)));
        services.AddScoped<IEmailSenderService, EmailSenderService>();


    }
}
