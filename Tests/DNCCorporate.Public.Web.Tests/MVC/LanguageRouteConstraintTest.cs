using DNCCorporate.Public.Web.Infrastructure.MVC;
using DNCCorporate.Server.Contract;
using Microsoft.AspNetCore.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace DNCCorporate.Public.Web.Tests.MVC
{
    [TestClass]
    public class MyTestClass
    {
        [TestMethod]
        public void ConstraintNotPresent()
        {
            var languageProviderMock = new Mock<ILanguageProvider>();

            var constraint = new LanguageRouteConstraint(languageProviderMock.Object);
            Assert.IsFalse(constraint.Match(null, null, null, new RouteValueDictionary(),
                RouteDirection.IncomingRequest));
        }

        [TestMethod]
        public void ConstraintPresentNoLanguage()
        {
            var languageProviderMock = new Mock<ILanguageProvider>();
            languageProviderMock.Setup(x => x.IsLanguageAvailable(It.IsAny<string>()))
                .Returns(false);

            var constraint = new LanguageRouteConstraint(languageProviderMock.Object);
            Assert.IsFalse(constraint.Match(null, null, null, new RouteValueDictionary(new Dictionary<string, string>()
            {
                { LanguageRouteConstraint.ROUTE_LABEL , "en" }
            }),
            RouteDirection.IncomingRequest));
        }

        [TestMethod]
        public void ConstratintPresentLanguageFound()
        {
            var languageProviderMock = new Mock<ILanguageProvider>();
            languageProviderMock.Setup(x => x.IsLanguageAvailable(It.IsAny<string>()))
                .Returns(true);

            var constraint = new LanguageRouteConstraint(languageProviderMock.Object);
            Assert.IsTrue(constraint.Match(null, null, null, new RouteValueDictionary(new Dictionary<string, string>()
            {
                { LanguageRouteConstraint.ROUTE_LABEL , "en" }
            }),
            RouteDirection.IncomingRequest));
        }
    }
}
