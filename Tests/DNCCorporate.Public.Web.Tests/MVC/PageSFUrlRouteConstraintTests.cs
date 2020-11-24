using DNCCorporate.Public.Web.Infrastructure.MVC;
using DNCCorporate.Server.Contract.Content;
using DNCCorporate.ViewModel;
using Microsoft.AspNetCore.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace DNCCorporate.Public.Web.Tests.MVC
{
    [TestClass]
    public class PageSFUrlRouteConstraintTests
    {
        [TestMethod]
        public void ConstraintNotPresent()
        {
            var languageProviderMock = new Mock<IPageService>();

            var constraint = new PageSFUrlRouteConstraint(languageProviderMock.Object);
            Assert.IsFalse(constraint.Match(null, null, null, new RouteValueDictionary(),
                RouteDirection.IncomingRequest));
        }

        [TestMethod]
        public void ConstraintPresentNoPage()
        {
            var pageServiceMock = new Mock<IPageService>();
            pageServiceMock.Setup(x => x.GetPage(It.IsAny<string>()))
                .Returns(null as PageViewModel);

            var constraint = new PageSFUrlRouteConstraint(pageServiceMock.Object);
            Assert.IsFalse(constraint.Match(null, null, null, new RouteValueDictionary(new Dictionary<string, string>()
            {
                { PageSFUrlRouteConstraint.ROUTE_LABEL , "page_url" }
            }),
            RouteDirection.IncomingRequest));
        }

        [TestMethod]
        public void ConstratintPresentPageFound()
        {
            var pageServiceMock = new Mock<IPageService>();
            pageServiceMock.Setup(x => x.GetPage(It.IsAny<string>()))
                .Returns(new PageViewModel());

            var constraint = new PageSFUrlRouteConstraint(pageServiceMock.Object);
            Assert.IsTrue(constraint.Match(null, null, null, new RouteValueDictionary(new Dictionary<string, string>()
            {
                { PageSFUrlRouteConstraint.ROUTE_LABEL , "page_url" }
            }),
            RouteDirection.IncomingRequest));
        }
    }
}
