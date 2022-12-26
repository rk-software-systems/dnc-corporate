using System.Collections.Immutable;

namespace DNCCorporate.Public.Web.Framework.Localization
{
    public class LocalizationSettings
    {
        public string DefaultCulture { get; set; }

        public string AvailableCulturesStr { get; set; }

        public ImmutableArray<string> AvailableCultures
        {
            get
            {
                if (!string.IsNullOrEmpty(AvailableCulturesStr))
                {
                    return AvailableCulturesStr
                        .Split(';')
                        .ToImmutableArray();
                }
                return ImmutableArray<string>.Empty;
            }
        }
    }
}
