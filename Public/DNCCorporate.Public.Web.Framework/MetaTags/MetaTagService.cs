using DNCCorporate.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using System.Collections.Concurrent;

namespace DNCCorporate.Public.Web.Framework
{
    public class MetaTagService : IMetaTagService
    {
        private ConcurrentDictionary<string, string> _metaTags = new ConcurrentDictionary<string, string>();
        private string _title = string.Empty;
        private readonly ITextResourceQueryService _textResourcesService;
        private readonly HttpContext _httpContext;

        public MetaTagService(ITextResourceQueryService textResourcesService,
            IHttpContextAccessor httpContextAccessor)
        {
            _textResourcesService = textResourcesService;
            if (httpContextAccessor?.HttpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContextAccessor));
            }

            _httpContext = httpContextAccessor.HttpContext;
        }

        private void AddTitle(string value, bool replace = false)
        {
            ArgumentNullException.ThrowIfNull(value, nameof(value));

            if (!string.IsNullOrEmpty(_title))
            {
                if (replace)
                {
                    _title = value;
                }
                else
                {
                    _title = $"{_title} | {value}";
                }
            }
            else
            {
                _title = value;
            }
        }

        public string? Title => _title;

        private void AddMetaTag(string key, string value, bool replace = false)
        {
            ArgumentNullException.ThrowIfNull(key, nameof(key));
            ArgumentNullException.ThrowIfNull(value, nameof(value));

            _metaTags.AddOrUpdate(key, value, (existingKey, existingValue) =>
            {
                return replace ? value : $"{existingValue} | {value}";
            });
        }

        public string? GetMetaTag(string key)
        {
            _metaTags.TryGetValue(key, out string? value);
            return value;
        }

        public void SetPageMetaTags(string pageName)
        {
            var title = _textResourcesService.GetTextResource("en", $"metatags.{pageName}.title") ?? pageName;
            AddTitle(title);
            string description = _textResourcesService.GetTextResource("en", $"metatags.{pageName}.description") ?? pageName;
            AddMetaTag("description", description, true);
            AddMetaTag("keywords", _textResourcesService.GetTextResource("en", $"metatags.{pageName}.keywords") ?? "", true);
            AddMetaTag("og:title", title, true);
            AddMetaTag("og:description", description, true);
            AddMetaTag("twitter:description", description, true);
            string ogImage = _httpContext.ThemeContentPath() + "img/custom/RK_Color_RGB.png";
            AddMetaTag("og:image", ogImage, true);
            AddMetaTag("twitter:card", ogImage, true);
            AddMetaTag("twitter:image", ogImage, true);
            AddMetaTag("og:type", "website", true);
            string url = _httpContext.Request.GetDisplayUrl();
            AddMetaTag("og:url", url, true);
            AddMetaTag("twitter:url", url, true);
            AddMetaTag("twitter:domain", _httpContext.Request.Host.Host, true);
            AddMetaTag("twitter:title", title, true);
        }
    }
}
