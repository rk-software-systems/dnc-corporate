using DNCCorporate.Server.Contract.Content;
using DNCCorporate.ViewModel;

namespace DNCCorporate.Server.Services
{
    public class PageService : IPageService
    {
        public PageViewModel GetDefaultPortalPage()
        {
            return new PageViewModel
            {
                SystemName = "Home",
                SFUrl = "home",
                Layout = "_Layout",
                Priority = 1,
                ViewName = null
            };
        }

        public PageViewModel GetPage(string sfUrl)
        {
            return new PageViewModel
            {
                SystemName = "Home",
                SFUrl = "home",
                Layout = "_Layout",
                Priority = 1,
                ViewName = null
            };
        }
    }
}
