using System.Collections.Frozen;
using System.Collections.Immutable;
using System.Text.Json;
using Microsoft.Extensions.Options;

namespace DNCCorporate.Services;

public class TextResourceQueryService : ITextResourceQueryService
{
    #region fields       
    
    private static FrozenDictionary<string, FrozenDictionary<string, string?>>? _textResources;
    private readonly ThemeSettings _themeSettings;
    private readonly LocalizationSettings _localizationSettings;

    #endregion

    #region ctors

    public TextResourceQueryService(
        IOptions<ThemeSettings> themeSettingsOptions,
        IOptions<LocalizationSettings> localizationSettingsOptions)
    {
        ArgumentNullException.ThrowIfNull(themeSettingsOptions, nameof(themeSettingsOptions));
        ArgumentNullException.ThrowIfNull(localizationSettingsOptions, nameof(localizationSettingsOptions));

        _themeSettings = themeSettingsOptions.Value;
        _localizationSettings = localizationSettingsOptions.Value;
    }

    #endregion

    #region methods

    public async Task LoadAll()
    {
        var textResources = new Dictionary<string, FrozenDictionary<string, string?>>();

        var files = GetFiles();
        foreach (var file in files)
        {
            using var fileStream = File.OpenRead(file.Value);
            var obj = await JsonSerializer.DeserializeAsync<Dictionary<string, JsonElement>>(fileStream);
            if (obj != null)
            {
                var cultureTextResources = obj.FromJsonObject()
                    .ToFrozenDictionary();

                textResources.Add(file.Key, cultureTextResources);
            }
        }
        _textResources = textResources.ToFrozenDictionary();
    }

    public FrozenDictionary<string, string?> GetTextResources(string culture)
    {
        if(_textResources != null && _textResources.Count > 0 && _textResources.TryGetValue(culture, out var dic))
        {
            return dic;
        }

        return FrozenDictionary<string, string?>.Empty;
    }

    public string? GetTextResource(string culture, string key)
    {
        var textResources = GetTextResources(culture);
        if (textResources.Count > 0 && textResources.TryGetValue(key, out var tr))
        {
            return tr;
        }

        return null;
    }

    #endregion

    #region helpers

    private ImmutableDictionary<string, string> GetFiles()
    {
        var path = GetFileDirectory();
        var files = Directory.GetFiles(path, "*.json")
            .Select(x =>
            {
                var fileName = Path.GetFileNameWithoutExtension(x);
                var culture = _localizationSettings.AvailableCultures.FirstOrDefault(y => y.Equals(fileName, StringComparison.OrdinalIgnoreCase));
                return new
                {
                    Key = culture,
                    Value = x
                };
            })
            .Where(x => x.Key != null)
            .ToImmutableDictionary(x => x.Key!, x => x.Value);

        return files;
    }

    private string GetFileDirectory()
    {
        return Path.Combine("Themes", _themeSettings.CurrentTheme, "i18n");
    }
    #endregion

}
