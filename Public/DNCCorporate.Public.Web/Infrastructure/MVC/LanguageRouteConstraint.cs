using DNCCorporate.Server.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace DNCCorporate.Public.Web.Infrastructure.MVC
{
    /// <summary>
    /// This route constraint is used to detect if language is a valid portal language
    /// </summary>
    public class LanguageRouteConstraint : IRouteConstraint
    {
        private readonly ILanguageProvider _languageProvider;

        public static string ROUTE_LABEL = "lang";

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
            if (!values.ContainsKey(ROUTE_LABEL))
            {
                return false;
            }

            var languageValue = values[ROUTE_LABEL].ToString();

            return _languageProvider.IsLanguageAvailable(languageValue);
        }
    }
}
