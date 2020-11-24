using DNCCorporate.Public.Web.Infrastructure.MVC;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DNCCorporate.Public.Web.Tests.MVC
{
    [TestClass]
    public class ViewLocationExpanderTests
    {

        [TestMethod]
        public void TestLocationPathSequence()
        {
            // Arrange
            var services = new ServiceCollection();
            services.AddMvc();

            // These two are required to active the RazorViewEngineOptions.
            services.AddSingleton<IHostEnvironment, HostingEnvironment>();
            services.AddSingleton<ILoggerFactory, LoggerFactory>();

            var serviceProvider = services.BuildServiceProvider();
            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.ViewLocationExpanders.Add(new ViewLocationExpander());
            });

            var viewEngine = serviceProvider.GetRequiredService<IViewEngine>();

            viewEngine.FindView(null, "", true);
        }
    }
}
