namespace DNCCorporate.Public.Web.Framework
{
    public enum TagHelperType
    {
        None = 0,
        // Title meta tag
        Title = 1,
        // <meta name="{locator}"
        NameBased = 2,
        // <meta property="{locator}"
        PropertyBased = 3
    }
}
