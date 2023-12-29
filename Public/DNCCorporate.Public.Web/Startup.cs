using DNCCorporate.Public.Web.Framework.Localizations;
using DNCCorporate.Public.Web.Framework.TextResources;
using DNCCorporate.Public.Web.Framework.ThemeCustomizations;
using DNCCorporate.Public.Web.Infrastructure;
using Microsoft.AspNetCore.Mvc.Razor;

namespace DNCCorporate.Public.Web;

public class Startup(IConfiguration configuration)
{
    public IConfiguration Configuration { get; } = configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        // settings
        services.Configure<LocalizationSettings>(Configuration.GetSection(nameof(LocalizationSettings)));
        services.Configure<ThemeSettings>(Configuration.GetSection(nameof(ThemeSettings)));
        
        // services
        services.RegisterDNCServices(configuration);

        // infrastructure            
        var localizationSettings = Configuration.GetSection(nameof(LocalizationSettings))
            .Get<LocalizationSettings>();

        services.ConfigureRequestLocalization(localizationSettings);

        var themeSettings = Configuration.GetSection(nameof(ThemeSettings))
            .Get<ThemeSettings>();

        services.Configure<RazorViewEngineOptions>(options =>
        {
            options.ViewLocationExpanders.Add(new ViewLocationExpander(themeSettings));
        });

        services.AddSingleton<TextResourceCultureLocalizer>();

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
