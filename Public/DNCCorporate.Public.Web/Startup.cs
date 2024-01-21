using DNCCorporate.Public.Web.Framework.Localizations;
using DNCCorporate.Public.Web.Framework.ThemeCustomizations;
using DNCCorporate.Public.Web.Infrastructure;
using DNCCorporate.Services;
using Microsoft.AspNetCore.Mvc.Razor;

namespace DNCCorporate.Public.Web;

public class Startup(IConfiguration configuration)
{
    public IConfiguration Configuration { get; } = configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        // services
        services.RegisterDNCServices(configuration);

        // localization            
        var localizationSettings = Configuration.GetSection(nameof(LocalizationSettings))
            .Get<LocalizationSettings>();
        services.ConfigureRequestLocalization(localizationSettings);

        // theme
        var themeSettings = Configuration.GetSection(nameof(ThemeSettings))
            .Get<ThemeSettings>();
        services.Configure<RazorViewEngineOptions>(options =>
        {
            options.ViewLocationExpanders.Add(new ViewLocationExpander(themeSettings));
        });
       

        services.AddRazorPages()
                .AddViewLocalization(o => o.ResourcesPath = $"Themes/{themeSettings.CurrentTheme}")
                .AddRazorPagesOptions(o => {
                    o.Conventions.Add(new CultureTemplateRouteModelConvention());
                });            
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (!env.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseRequestLocalization();
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapRazorPages();
        });
    }
}
