using DNCCorporate.Contracts;
using Microsoft.Extensions.DependencyInjection;

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
    public static void RegisterDNCServices(this IServiceCollection services)
    {
        services.AddTransient<IWorkContext, WebWorkContext>();
    }
}
