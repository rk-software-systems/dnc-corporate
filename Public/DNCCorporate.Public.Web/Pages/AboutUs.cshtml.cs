using DNCCorporate.Public.Web.Framework;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DNCCorporate.Public.Web.Pages;

public class AboutUsModel : PageModel
{
    private readonly IMetaTagService _metaTagService;

    public AboutUsModel(IMetaTagService metaTagService)
    {
        _metaTagService = metaTagService;
    }

    public void OnGet()
    {
        _metaTagService.SetPageMetaTags(PageName);
    }

    public const string PageName = "aboutus";
}
