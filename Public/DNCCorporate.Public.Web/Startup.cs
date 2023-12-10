﻿using DNCCorporate.Public.Web.Framework.Localization;
using DNCCorporate.Public.Web.Framework.TextResource;
using DNCCorporate.Public.Web.Framework.ThemeCustomization;
using DNCCorporate.Public.Web.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DNCCorporate.Public.Web;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        // settings
        services.Configure<LocalizationSettings>(Configuration.GetSection(nameof(LocalizationSettings)));
        services.Configure<ThemeSettings>(Configuration.GetSection(nameof(ThemeSettings)));

        // services
        services.RegisterDNCServices();

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
