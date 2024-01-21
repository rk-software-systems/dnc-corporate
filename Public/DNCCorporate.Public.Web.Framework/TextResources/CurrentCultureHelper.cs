using System.Globalization;

namespace DNCCorporate.Public.Web.Framework;

public static class CurrentCultureHelper
{
    public static string CurrentCulture => CultureInfo.CurrentCulture.Name;
}
