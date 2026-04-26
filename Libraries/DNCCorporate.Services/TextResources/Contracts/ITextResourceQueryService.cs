using System.Collections.Frozen;

namespace DNCCorporate.Services;

public interface ITextResourceQueryService
{
    Task LoadAll();

    FrozenDictionary<string, string?> GetTextResources(string culture);

    string? GetTextResource(string culture, string key);
}
