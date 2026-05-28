using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS.DataEngine;
using CMS.Websites;
using CMS.Websites.Routing;
using Kentico.Content.Web.Mvc;
using Kentico.Content.Web.Mvc.Routing;
using Kentico.PageBuilder.Web.Mvc;
using Microsoft.AspNetCore.Mvc;

[assembly: RegisterWidget(
    identifier: "LearnHub.PricingWidget",
    viewComponentType: typeof(LearnHub.Widgets.PricingWidgetViewComponent),
    name: "Pricing Widget",
    propertiesType: typeof(LearnHub.Widgets.PricingWidgetProperties),
    Description = "Displays pricing plans",
    IconClass = "xp-dollar")]

namespace LearnHub.Widgets
{
    public class PricingWidgetViewComponent : ViewComponent
    {
        private readonly IContentRetriever contentRetriever;
        private readonly IWebPageUrlRetriever webPageUrlRetriever;
        private readonly IWebsiteChannelContext websiteChannelContext;
        private readonly IPreferredLanguageRetriever preferredLanguageRetriever;

        public PricingWidgetViewComponent(
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

        public async Task<IViewComponentResult> InvokeAsync(PricingWidgetProperties properties)
        {
            var viewModel = new PricingWidgetViewModel();

            var prices = await contentRetriever.RetrievePages<Price>(
                new RetrievePagesParameters
                {
                    LinkedItemsMaxLevel = 1
                },
                query => query.TopN(properties.NumberOfPlans),
                new RetrievalCacheSettings($"PricingWidget_{properties.NumberOfPlans}", TimeSpan.FromMinutes(5)),
                HttpContext.RequestAborted
            );

            if (!prices.Any())
            {
                return View("~/Components/Widgets/PricingWidget/_PricingWidget.cshtml", viewModel);
            }

            // Get current language
            var languageName = preferredLanguageRetriever.Get();

            // Collect all WebPageGuids from ButtonLink
            var linkedPageGuids = prices
                .Where(x => x.ButtonLink?.Any() == true)
                .SelectMany(x => x.ButtonLink.Select(y => y.WebPageGuid))
                .Distinct()
                .ToList();

            // Bulk retrieve URLs for all linked pages
            var urls = linkedPageGuids.Any()
                ? await webPageUrlRetriever.Retrieve(
                    [.. linkedPageGuids],
                    websiteChannelContext.WebsiteChannelName,
                    languageName,
                    websiteChannelContext.IsPreview,
                    HttpContext.RequestAborted)
                : new Dictionary<System.Guid, WebPageUrl>();

            foreach (var price in prices)
            {
                var itemList = !string.IsNullOrEmpty(price.PriceItemList)
                    ? price.PriceItemList.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(item => item.Trim())
                        .Where(item => !string.IsNullOrEmpty(item))
                        .ToList()
                    : new System.Collections.Generic.List<string>();

                // Get button link from ButtonLink property
                string buttonLink = "#";
                if (price.ButtonLink?.Any() == true)
                {
                    var linkedPageGuid = price.ButtonLink.First().WebPageGuid;
                    if (urls.ContainsKey(linkedPageGuid))
                    {
                        buttonLink = urls[linkedPageGuid].RelativePath;
                    }
                }

                viewModel.PricingPlans.Add(new PricingCardViewModel
                {
                    Title = price.PriceTitle ?? string.Empty,
                    Description = price.PriceDescription ?? string.Empty,
                    PricePerMonth = price.PricePerMonth,
                    ItemList = itemList,
                    ButtonName = price.ButtonName ?? "Learn More",
                    ButtonLink = buttonLink,
                    PageUrl = "#",  // Can be enhanced to link to actual price pages
                    IsMostPopular = price.PriceMostPopular
                });
            }

            return View("~/Components/Widgets/PricingWidget/_PricingWidget.cshtml", viewModel);
        }
    }
}
