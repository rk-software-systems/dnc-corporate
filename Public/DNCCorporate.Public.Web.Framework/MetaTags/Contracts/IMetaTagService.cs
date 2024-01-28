namespace DNCCorporate.Public.Web.Framework
{
    public interface IMetaTagService
    {
        void AddTitle(string value, bool replace = false);

        string? Title { get; }

        void AddMetaTag(string key, string value, bool replace = false);

        string? GetMetaTag(string key);

        void RemoveMetaTag(string key);
    }
}