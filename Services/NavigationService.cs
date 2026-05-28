using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CMS.Websites;
using CMS.Websites.Routing;
using Kentico.Content.Web.Mvc;
using Kentico.Content.Web.Mvc.Routing;
using LearnHub;

namespace LearnHub.Services
{
    public interface INavigationService
    {
        Task<IEnumerable<NavigationItem>> GetNavigationItemsAsync(CancellationToken cancellationToken = default);
    }

    public class NavigationService : INavigationService
    {
        private readonly IContentRetriever contentRetriever;
        private readonly IWebPageUrlRetriever webPageUrlRetriever;
        private readonly IWebsiteChannelContext websiteChannelContext;
        private readonly IPreferredLanguageRetriever preferredLanguageRetriever;

        public NavigationService(
            IContentRetriever contentRetriever,
            IWebPageUrlRetriever webPageUrlRetriever,
            IWebsiteChannelContext websiteChannelContext,
            IPreferredLanguageRetriever preferredLanguageRetriever)
        {
            this.contentRetriever = contentRetriever;
            this.webPageUrlRetriever = webPageUrlRetriever;
            this.websiteChannelContext = websiteChannelContext;
            this.preferredLanguageRetriever = preferredLanguageRetriever;
        }

        public async Task<IEnumerable<NavigationItem>> GetNavigationItemsAsync(CancellationToken cancellationToken = default)
        {
            var navigationPages = await contentRetriever.RetrievePages<Navigation>(
                new RetrievePagesParameters(),
                query => query.TopN(10),
                new RetrievalCacheSettings("Navigation", System.TimeSpan.FromMinutes(30)),
                cancellationToken
            );

            if (!navigationPages.Any())
            {
                return Enumerable.Empty<NavigationItem>();
            }

            // Get current language
            var languageName = preferredLanguageRetriever.Get();

            // Collect all WebPageGuids from NavigationItemLink
            var linkedPageGuids = navigationPages
                .Where(x => x.NavigationItemLink?.Any() == true)
                .SelectMany(x => x.NavigationItemLink.Select(y => y.WebPageGuid))
                .Distinct()
                .ToList();

            // Bulk retrieve URLs for all linked pages
            var urls = linkedPageGuids.Any()
                ? await webPageUrlRetriever.Retrieve(
                    [.. linkedPageGuids],
                    websiteChannelContext.WebsiteChannelName,
                    languageName,
                    websiteChannelContext.IsPreview,
                    cancellationToken)
                : new Dictionary<System.Guid, WebPageUrl>();

            // Map navigation items with their URLs
            var navigationItems = new List<NavigationItem>();

            foreach (var navPage in navigationPages)
            {
                string url = "#";
                
                if (navPage.NavigationItemLink?.Any() == true)
                {
                    var linkedPageGuid = navPage.NavigationItemLink.First().WebPageGuid;
                    if (urls.ContainsKey(linkedPageGuid))
                    {
                        url = urls[linkedPageGuid].RelativePath;
                    }
                }

                navigationItems.Add(new NavigationItem
                {
                    Name = navPage.NavigationItemName ?? string.Empty,
                    Url = url
                });
            }

            return navigationItems;
        }
    }

    public class NavigationItem
    {
        public string Name { get; set; } = string.Empty;
        public string Url { get; set; } = "#";
    }
}
