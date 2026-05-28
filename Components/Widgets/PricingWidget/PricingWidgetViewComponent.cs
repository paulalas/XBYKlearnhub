using System;
using System.Linq;
using System.Threading.Tasks;
using CMS.DataEngine;
using Kentico.Content.Web.Mvc;
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

        public PricingWidgetViewComponent(IContentRetriever contentRetriever)
        {
            this.contentRetriever = contentRetriever;
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

            foreach (var price in prices)
            {
                var itemList = !string.IsNullOrEmpty(price.PriceItemList)
                    ? price.PriceItemList.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(item => item.Trim())
                        .Where(item => !string.IsNullOrEmpty(item))
                        .ToList()
                    : new System.Collections.Generic.List<string>();

                var buttonLink = "#!";
                // ButtonLink handling can be enhanced later if needed

                viewModel.PricingPlans.Add(new PricingCardViewModel
                {
                    Title = price.PriceTitle ?? string.Empty,
                    Description = price.PriceDescription ?? string.Empty,
                    PricePerMonth = price.PricePerMonth,
                    ItemList = itemList,
                    ButtonName = price.ButtonName ?? "Learn More",
                    ButtonLink = buttonLink,
                    PageUrl = "#"  // Can be enhanced to link to actual price pages
                });
            }

            return View("~/Components/Widgets/PricingWidget/_PricingWidget.cshtml", viewModel);
        }
    }
}
