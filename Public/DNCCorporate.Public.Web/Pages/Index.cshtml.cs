using DNCCorporate.Public.Web.Framework;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DNCCorporate.Public.Web.Pages;

public class IndexModel : PageModel
{
    private readonly IMetaTagService _metaTagService;

    public IndexModel(IMetaTagService metaTagService)
    {
        _metaTagService = metaTagService;
    }

    public const string PageName = "home";

    public void OnGet()
    {
        _metaTagService.SetPageMetaTags(PageName);
    }
}
