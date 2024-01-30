using DNCCorporate.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;
using SimpleSiteMap;

namespace DNCCorporate.Public.Web.Framework;

public class SitemapService : ISitemapService
{
    #region fields    

    private readonly LocalizationSettings _localizationSettings;
    private readonly EndpointDataSource _endpointsDataSource;
    private readonly IApplicationDateService _applicationDateService;
    #endregion

    #region ctors

    public SitemapService(
        IApplicationDateService applicationDateService,
        IOptions<LocalizationSettings> localizationSettingsOptions,
        EndpointDataSource EndpointsDataSource)
    {
        ArgumentNullException.ThrowIfNull(localizationSettingsOptions, nameof(localizationSettingsOptions));

        _localizationSettings = localizationSettingsOptions.Value;
        _endpointsDataSource = EndpointsDataSource;
        _applicationDateService = applicationDateService;
    }
    #endregion

    #region methods

    public string GetSitemap(Uri baseUrl)
    {
        var nodes = new List<SitemapNode>();
        var indexNode = new SitemapNode(baseUrl, _applicationDateService.StartedOnUtc, SitemapFrequency.Monthly, 1, false);
        nodes.Add(indexNode);

        var endpoints = GetAvilableEndpoints();
        foreach (var endpoint in endpoints)
        {
            var node = new SitemapNode(new Uri(baseUrl, endpoint), _applicationDateService.StartedOnUtc, SitemapFrequency.Monthly, 0.9, false);
            nodes.Add(node);

            foreach (var culture in _localizationSettings.AvailableCultures)
            {
                var cultureNode = new SitemapNode(new Uri(baseUrl, $"{culture}{endpoint}"), _applicationDateService.StartedOnUtc, SitemapFrequency.Monthly, 0.9, false);
                nodes.Add(cultureNode);
            }
        }

        var sitemapGenerator = new SimpleSiteMap.SitemapService();
        return sitemapGenerator.ConvertToXmlUrlset(nodes);
    }
    #endregion

    #region helpers

    private List<string> GetAvilableEndpoints()
    {
        var endpoints = _endpointsDataSource
            .Endpoints
            .SelectMany(x => x.Metadata)
            .Select(x => (x as PageActionDescriptor)?.ViewEnginePath)
            .Where(x => x != null)
            .Distinct()
            .ToList();

        return endpoints;
    }
    #endregion
}
