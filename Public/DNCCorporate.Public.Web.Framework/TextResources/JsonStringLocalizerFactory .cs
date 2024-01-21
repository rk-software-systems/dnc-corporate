using DNCCorporate.Services;
using Microsoft.Extensions.Localization;

namespace DNCCorporate.Public.Web.Framework.TextResources;

public class JsonStringLocalizerFactory(ITextResourceQueryService textResourceQueryService) : IStringLocalizerFactory
{
    #region fields      

    private readonly ITextResourceQueryService _textResourceQueryService = textResourceQueryService;

    #endregion

    #region methods

    public IStringLocalizer Create(Type resourceSource)
    {
        return new JsonStringLocalizer(_textResourceQueryService);
    }

    public IStringLocalizer Create(string baseName, string location)
    {
        return new JsonStringLocalizer(_textResourceQueryService);
    }

    #endregion
}
