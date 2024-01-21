using Microsoft.Extensions.Localization;

namespace DNCCorporate.Public.Web.Framework.TextResources;

public class TextResourceCultureLocalizer
{
    #region fields  

    private readonly IStringLocalizer _localizer;

    #endregion

    #region props 

#pragma warning disable CA1822 // Mark members as static
    public string CurrentCulture => CurrentCultureHelper.CurrentCulture;
#pragma warning restore CA1822 // Mark members as static

    #endregion

    #region ctors

    public TextResourceCultureLocalizer(IStringLocalizerFactory factory)
    {
        ArgumentNullException.ThrowIfNull(factory, nameof(factory));

        _localizer = factory.Create(string.Empty, string.Empty);
    }
    #endregion

    #region methods

    public LocalizedString Get(string key, params string[] arguments)
    {
        return arguments == null
            ? _localizer[key]
            : _localizer[key, arguments];
    }

    #endregion
}
