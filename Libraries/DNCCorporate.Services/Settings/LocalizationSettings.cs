using System.Collections.Frozen;

namespace DNCCorporate.Services;

public class LocalizationSettings
{
    public required string DefaultCulture { get; set; }

    public required string AvailableCulturesStr { get; set; }

    public FrozenSet<string> AvailableCultures
    {
        get
        {
            if (!string.IsNullOrEmpty(AvailableCulturesStr))
            {
                return AvailableCulturesStr
                    .Split(';')
                    .ToFrozenSet();
            }
            return [];
        }
    }
}
