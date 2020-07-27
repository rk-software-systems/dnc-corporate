namespace DNCCorporate.ViewModel
{
    /// <summary>
    /// This view model represents portal page
    /// </summary>
    public class PageViewModel
    {
        /// <summary>
        /// Page system name
        /// </summary>
        public string SystemName { get; set; }

        /// <summary>
        /// Page SEO Friendly URL
        /// </summary>
        public string SFUrl { get; set; }

        /// <summary>
        /// Page priority
        /// </summary>
        public double Priority { get; set; }

        /// <summary>
        /// Page Layout (this layout file is going to be used in page view rendering)
        /// </summary>
        public string Layout { get; set; }

        /// <summary>
        /// Use specific page name (in case page is not configured with dynamic layout)
        /// </summary>
        public string ViewName { get; set; }
    }
}
