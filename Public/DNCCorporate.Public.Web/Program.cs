using DNCCorporate.Public.Web.Framework.Localizations;
using DNCCorporate.Public.Web.Framework.ThemeCustomizations;
using DNCCorporate.Public.Web.Infrastructure;
using DNCCorporate.Services;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.Razor;


var builder = WebApplication.CreateBuilder(args);

// services
builder.Services.RegisterDNCServices(builder.Configuration);

// localization            
var localizationSettings = builder.Configuration.GetSection(nameof(LocalizationSettings))
    .Get<LocalizationSettings>();
builder.Services.ConfigureRequestLocalization(localizationSettings);

// theme
var themeSettings = builder.Configuration.GetSection(nameof(ThemeSettings))
    .Get<ThemeSettings>();
builder.Services.Configure<RazorViewEngineOptions>(options =>
{
    options.ViewLocationExpanders.Add(new ViewLocationExpander(themeSettings));
});

//Configure headers get correct RemoteIpAddress
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto | ForwardedHeaders.XForwardedHost;
    options.KnownNetworks.Clear();
    options.KnownProxies.Clear();
});

builder.Services.AddRazorPages()
        .AddViewLocalization(o => o.ResourcesPath = $"Themes/{themeSettings.CurrentTheme}")
        .AddRazorPagesOptions(o => {
            o.Conventions.Add(new CultureTemplateRouteModelConvention());
        });

var app = builder.Build();

// init text resources
var textResourceQueryService = app.Services.GetRequiredService<ITextResourceQueryService>();
await textResourceQueryService.LoadAll();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseRequestLocalization();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapRazorPages();

await app.RunAsync();
