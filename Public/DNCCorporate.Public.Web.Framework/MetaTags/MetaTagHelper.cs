using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DNCCorporate.Public.Web.Framework
{
    [HtmlTargetElement(Attributes = MetaTagsAttributeName)]
    public class MetaTagHelper : TagHelper
    {
        private const string MetaTagsAttributeName = "dnc-meta";

        private readonly IMetaTagService _metaTagsService;

        public TagHelperType DncMetaType { get; set; }

        public MetaTagHelper(IMetaTagService metaTagsService)
        {
            _metaTagsService = metaTagsService;
        }

        public async override Task ProcessAsync(TagHelperContext context,
            TagHelperOutput output)
        {
            ArgumentNullException.ThrowIfNull(output, nameof(output));
            ArgumentNullException.ThrowIfNull(context, nameof(context));

            switch (DncMetaType)
            {
                case TagHelperType.Title:
                    output.Content.SetContent(_metaTagsService.Title);
                    break;
                case TagHelperType.NameBased:
                    GetAttributeValueAndSetContent(output, "name", _metaTagsService.GetMetaTag);
                    break;
                case TagHelperType.PropertyBased:
                    GetAttributeValueAndSetContent(output, "property", _metaTagsService.GetMetaTag);
                    break;
            }

            await base.ProcessAsync(context, output);
        }

        private static void GetAttributeValueAndSetContent(TagHelperOutput output, string attributeName, Func<string, string?> attributeGetter)
        {
            if (output.Attributes.TryGetAttribute(attributeName, out TagHelperAttribute propertyAttribute)
                && !string.IsNullOrEmpty(propertyAttribute.Value?.ToString()))
            {
                var metaTag = attributeGetter(propertyAttribute.Value?.ToString() ?? "");
                if (!string.IsNullOrEmpty(metaTag))
                {
                    output.Attributes.SetAttribute("content", metaTag);
                }
            }
        }
    }
}
