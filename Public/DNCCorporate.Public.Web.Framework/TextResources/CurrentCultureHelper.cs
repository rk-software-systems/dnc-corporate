using System.Globalization;

namespace DNCCorporate.Public.Web.Framework.TextResources;

public static class CurrentCultureHelper
{
    public static string CurrentCulture => CultureInfo.CurrentCulture.Name;
}
