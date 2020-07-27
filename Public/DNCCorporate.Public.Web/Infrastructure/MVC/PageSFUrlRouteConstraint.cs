using DNCCorporate.Server.Contract.Content;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace DNCCorporate.Public.Web.Infrastructure.MVC
{
    /// <summary>
    /// This constraint is used to detect if page with provided SEO friendly URL is present in System
    /// </summary>
    public class PageSFUrlRouteConstraint : IRouteConstraint
    {
        private readonly IPageService _pageService;

        public static string ROUTE_LABEL = "pageurl";

        /// <summary>
        /// Initializes a new instance of the <see cref="PageSFUrlRouteConstraint"/> class.
        /// </summary>
        /// <param name="pageService"><see cref="IPageService"/></param>
        public PageSFUrlRouteConstraint(IPageService pageService)
        {
            _pageService = pageService;
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

            var pageSFUrl = values[ROUTE_LABEL]?.ToString();

            var page = _pageService.GetPage(pageSFUrl);

            if (page == null)
            {
                return false;
            }

            return true;
        }
    }
}
