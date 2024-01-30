using System.Net.Mime;
using DNCCorporate.Public.Web.Framework;
using DNCCorporate.Public.Web.Infrastructure;
using DNCCorporate.Services;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Options;

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

builder.Services.AddRazorPages()    
        .AddViewLocalization(o => o.ResourcesPath = $"Themes/{themeSettings.CurrentTheme}")
        .AddRazorPagesOptions(o =>
        {            
            o.Conventions.Add(new CultureTemplateRouteModelConvention());
        });
builder.Services.AddHttpContextAccessor();

//Configure headers get correct RemoteIpAddress
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto | ForwardedHeaders.XForwardedHost;
    options.KnownNetworks.Clear();
    options.KnownProxies.Clear();
});

var app = builder.Build();

// init application dates
var applicationDateService = app.Services.GetRequiredService<IApplicationDateService>();
var _logApplicationIsStartingOnInformation = LoggerMessage.Define<DateTime>(
       LogLevel.Information,
       10000001,
       "Application is starting on '{StartedOn}'");
_logApplicationIsStartingOnInformation(app.Logger, applicationDateService.StartedOnUtc, null);

// init text resources
var textResourceQueryService = app.Services.GetRequiredService<ITextResourceQueryService>();
await textResourceQueryService.LoadAll();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseForwardedHeaders();

app.UseHttpsRedirection();
app.UseStaticFiles();

// Configure the Minimal API
app.Map("/api", app =>
{
    app.UseRouting();
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapGet("/sitemap.xml", (HttpRequest request, ISitemapService sitemapService) =>
        {
            var baseUrl = new Uri($"{request.Scheme}://{request.Host}");
            var sitemap = sitemapService.GetSitemap(baseUrl);

            return Results.Content(sitemap, contentType: MediaTypeNames.Application.Xml, statusCode: StatusCodes.Status200OK);
        });
    });
});

// Configure Razor Pages
app.UseRequestLocalization();
app.MapRazorPages();

await app.RunAsync();


