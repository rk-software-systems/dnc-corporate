using DNCCorporate.ViewModel;

namespace DNCCorporate.Server.Contract.Content
{
    /// <summary>
    /// This service is used to process portal page related data
    /// </summary>
    public interface IPageService
    {
        /// <summary>
        /// Get page view model using its SEO friendly URL
        /// </summary>
        /// <param name="sfUrl">Page SEO friendly URL</param>
        /// <returns>Page view model</returns>
        PageViewModel GetPage(string sfUrl);

        /// <summary>
        /// Get page that is default for portal
        /// </summary>
        /// <returns>Page URL</returns>
        PageViewModel GetDefaultPortalPage();
    }
}
