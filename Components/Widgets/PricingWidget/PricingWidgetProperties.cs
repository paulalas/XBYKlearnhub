using Kentico.PageBuilder.Web.Mvc;
using Kentico.Xperience.Admin.Base.FormAnnotations;

namespace LearnHub.Widgets
{
    public class PricingWidgetProperties : IWidgetProperties
    {
        [NumberInputComponent(
            Label = "Number of pricing plans",
            Order = 1)]
        public int NumberOfPlans { get; set; } = 3;
    }
}
