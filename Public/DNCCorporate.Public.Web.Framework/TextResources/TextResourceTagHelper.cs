using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DNCCorporate.Public.Web.Framework.TextResources;

[HtmlTargetElement(Attributes = TextResourceAttributeName)]
public class TextResourceTagHelper : TagHelper
{
    private const string TextResourceAttributeName = "dnc-tr";

    //private ITextServiceAdapter _textServiceAdapter;

    [HtmlAttributeName(TextResourceAttributeName)]
    public string TextResource { get; set; }

    public TextResourceTagHelper(
        //ITextServiceAdapter textServiceAdapter
        )
    {
        //_textServiceAdapter = textServiceAdapter;
    }

    public async override Task ProcessAsync(TagHelperContext context,
        TagHelperOutput output)
    {
        ArgumentNullException.ThrowIfNull(output, nameof(output));


        //var str = await _textServiceAdapter.GetResource(TextResource);
        //output.Content.SetContent(str);

        await base.ProcessAsync(context, output);
    }
}
