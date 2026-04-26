using DNCCorporate.Services;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DNCCorporate.Public.Web.Framework;

[HtmlTargetElement(Attributes = TextResourceAttributeName)]
public class TextResourceTagHelper(ITextResourceQueryService textResourceQueryService) : TagHelper
{
    #region consts  
    private const string TextResourceAttributeName = "dnc-tr";

    #endregion

    #region fields            
    
    private readonly ITextResourceQueryService _textResourceQueryService = textResourceQueryService;
    #endregion

    #region props  

    [HtmlAttributeName(TextResourceAttributeName)]
    public required string Key { get; set; }

    #endregion
    #region ctors
    #endregion

    #region methods

    public async override Task ProcessAsync(TagHelperContext context,
        TagHelperOutput output)
    {
        ArgumentNullException.ThrowIfNull(output, nameof(output));

        var culture = CurrentCultureHelper.CurrentCulture;
        var str = _textResourceQueryService.GetTextResource(culture, Key);
        output.Content.SetHtmlContent(str ?? Key);

        await base.ProcessAsync(context, output);
    }

    #endregion
}
