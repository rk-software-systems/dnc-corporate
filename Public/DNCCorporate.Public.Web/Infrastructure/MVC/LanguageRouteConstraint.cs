using DNCCorporate.Server.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Linq;

namespace DNCCorporate.Public.Web.Infrastructure.MVC
{
    public class LanguageRouteConstraint : IRouteConstraint
    {
        private readonly ILanguageProvider _languageProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="LanguageRouteConstraint"/> class.
        /// </summary>
        /// <param name="languageProvider"><see cref="ILanguageProvider"/></param>
        public LanguageRouteConstraint(ILanguageProvider languageProvider)
        {
            _languageProvider = languageProvider;
        }

        public bool Match(HttpContext httpContext,
            IRouter route,
            string routeKey,
            RouteValueDictionary values,
            RouteDirection routeDirection)
        {
            if (!values.ContainsKey("lang"))
            {
                return false;
            }

            var languageValue = values["lang"].ToString();

            return _languageProvider.GetAvailableLanguages()
                .Any(x => string.Compare(x, languageValue, System.StringComparison.InvariantCultureIgnoreCase) == 0);
        }
    }
}
