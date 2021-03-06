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

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">Services container</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.RegisterDNCServices();
            services.AddControllersWithViews();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">Application HTTP request pipeline</param>
        /// <param name="env">Hosting environment abstraction</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
                    "}/{*pageurl:" + PageSFUrlRouteConstraint.ROUTE_LABEL +"}",
                    defaults: new { controller = "Page", action = "Index" });

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{*anyslug}",
                    defaults: new { controller = "Home", action = "RedirectToDefaultLanguage" });
            });
        }
    }
}
