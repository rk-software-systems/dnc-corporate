using DNCCorporate.Server.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace DNCCorporate.Public.Web.Infrastructure
{
    /// <summary>
    /// DNC corporate Public website <see cref="IWorkContext"/> implementation
    /// </summary>
    public class WebWorkContext : IWorkContext
    {
        private string _workingLanguage;
        private readonly ILanguageProvider _languageProvider;
        private readonly HttpContext _httpContext;


        /// <summary>
        /// Initializes a new instance of the <see cref="WebWorkContext"/> class.
        /// </summary>
        /// <param name="contextAccessor"><see cref="IHttpContextAccessor"/></param>
        public WebWorkContext(IHttpContextAccessor contextAccessor,
            ILanguageProvider languageProvider)
        {
            _httpContext = contextAccessor.HttpContext;
            _languageProvider = languageProvider;
        }

        public string WorkingLanguage
        {
            get
            {
                if (string.IsNullOrEmpty(_workingLanguage))
                {
                    _workingLanguage = _httpContext.GetRouteValue("lang") as string;
                }

                return _workingLanguage;
            }
        }

        public string DefaultLanguage
        {
            get
            {
                return _languageProvider.GetDefaultLanguage();
            }
        }

        public string CurrentTheme
        {
            get
            {
                return "RKSoftware";
            }
        }
    }
}
