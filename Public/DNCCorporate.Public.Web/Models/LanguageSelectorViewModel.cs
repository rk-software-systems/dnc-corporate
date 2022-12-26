using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DNCCorporate.Public.Web.Models
{
    public record class LanguageSelectorViewModel(
        CultureInfo RequestCulture, 
        List<SelectListItem> SupportedCultures,
        Dictionary<string, string> RouteData);
}
