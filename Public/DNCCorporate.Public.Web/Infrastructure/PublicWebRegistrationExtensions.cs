using DNCCorporate.Public.Web.Infrastructure.MVC;
using DNCCorporate.Server.Contract;
using DNCCorporate.Server.Services.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace DNCCorporate.Public.Web.Infrastructure
{
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
            services.Configure<RouteOptions>(options =>
            {
                options.ConstraintMap.Add("lang", typeof(LanguageRouteConstraint));
            });

            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.ViewLocationExpanders.Add(new ViewLocationExpander());
            });

            services.AddHttpContextAccessor();
            services.AddTransient<ILanguageProvider, LanguageProvider>();
            services.AddTransient<IWorkContext, WebWorkContext>();
        }
    }
}
