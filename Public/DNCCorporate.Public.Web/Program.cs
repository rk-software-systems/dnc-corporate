using DNCCorporate.Public.Web;
using DNCCorporate.Services;

var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder.Configuration);

startup.ConfigureServices(builder.Services);

var app = builder.Build();

// init text resources
var textResourceQueryService = app.Services.GetRequiredService<ITextResourceQueryService>();
await textResourceQueryService.LoadAll();

startup.Configure(app, app.Environment);

await app.RunAsync();
