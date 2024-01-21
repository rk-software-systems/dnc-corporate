using System.Globalization;
using DNCCorporate.Services;
using Microsoft.Extensions.Localization;

namespace DNCCorporate.Public.Web.Framework.TextResources;

public class JsonStringLocalizer(ITextResourceQueryService textResourceQueryService) : IStringLocalizer
{
    #region fields     

    private readonly ITextResourceQueryService _textResourceQueryService = textResourceQueryService;

    #endregion

    #region props       

    public LocalizedString this[string name]
    {
        get
        {
            var culture = CurrentCultureHelper.CurrentCulture;
            var tr = _textResourceQueryService.GetTextResource(culture, name);
            return new LocalizedString(name, tr ?? name, false);
        }
    }

    public LocalizedString this[string name, params object[] arguments]
    {
        get
        {
            var culture = CurrentCultureHelper.CurrentCulture;
            var tr = _textResourceQueryService.GetTextResource(culture, name);
            if(tr != null)
            {
                tr = string.Format(CultureInfo.CurrentCulture, tr, arguments);
            }
            return new LocalizedString(name, tr ?? name, false);
        }
    }

    #endregion

    #region methods

    public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
    {
        var culture = CurrentCultureHelper.CurrentCulture;
        var textResources = _textResourceQueryService.GetTextResources(culture);

        return textResources.Select(x => new LocalizedString(x.Key, x.Value ?? x.Key, false));        
    }

    #endregion
}
