using System.Collections.Generic;

namespace LearnHub.Widgets
{
    public class PricingWidgetViewModel
    {
        public List<PricingCardViewModel> PricingPlans { get; set; } = new List<PricingCardViewModel>();
    }

    public class PricingCardViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal PricePerMonth { get; set; }
        public List<string> ItemList { get; set; } = new List<string>();
        public string ButtonName { get; set; }
        public string ButtonLink { get; set; }
        public bool IsMostPopular { get; set; }
        public string PageUrl { get; set; }
    }
}
