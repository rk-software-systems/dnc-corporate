namespace DNCCorporate.Public.Web.Framework
{
    public interface IMetaTagService
    {
        string? Title { get; }

        string? GetMetaTag(string key);

        void SetPageMetaTags(string pageName);
    }
}