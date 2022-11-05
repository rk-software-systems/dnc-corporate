using DNCCorporate.Public.Web.Infrastructure;
using DNCCorporate.Public.Web.Infrastructure.MVC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DNCCorporate.Public.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.RegisterDNCServices();
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (!env.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: RouteConstants.LOCALIZED_PAGE_ROUTE_NAME,
                    pattern: "{lang:" + LanguageRouteConstraint.ROUTE_LABEL +
                             "}/{*pageurl:" + PageSFUrlRouteConstraint.ROUTE_LABEL + "}",
                    defaults: new 
                    { 
                        controller = "Page", 
                        action = "Index" 
                    });

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{*anyslug}",
                    defaults: new 
                    { 
                        controller = "Home", 
                        action = "RedirectToDefaultLanguage" 
                    });
            });
        }
    }
}
