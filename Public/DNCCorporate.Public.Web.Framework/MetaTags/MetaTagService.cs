using System.Collections.Concurrent;

namespace DNCCorporate.Public.Web.Framework
{
    public class MetaTagService : IMetaTagService
    {
        private ConcurrentDictionary<string, string> _metaTags = new ConcurrentDictionary<string, string>();
        private string _title = string.Empty;

        public void AddTitle(string value, bool replace = false)
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

        public void AddMetaTag(string key, string value, bool replace = false)
        {
            ArgumentNullException.ThrowIfNull(key, nameof(key));
            ArgumentNullException.ThrowIfNull(value, nameof(value));

            _metaTags.AddOrUpdate(key, value, (existingKey, existingValue) =>
            {
                return replace ? value : $"{existingValue} | {value}";
            });
        }

        public void RemoveMetaTag(string key)
        {
            _metaTags.TryRemove(key, out _);
        }

        public string? GetMetaTag(string key)
        {
            _metaTags.TryGetValue(key, out string? value);
            return value;
        }
    }
}
