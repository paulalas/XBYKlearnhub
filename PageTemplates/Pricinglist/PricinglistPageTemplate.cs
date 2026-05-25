using Kentico.PageBuilder.Web.Mvc.PageTemplates;

[assembly: RegisterPageTemplate(
    identifier: "LearnHub.Pricinglist_Default",
    name: "Pricing List Page - Default",
    propertiesType: typeof(PageTemplates.Pricinglist.PricinglistPageTemplateProperties),
    customViewName: "~/PageTemplates/Pricinglist/_Pricinglist.cshtml",
    ContentTypeNames = new[] { LearnHub.Pricinglist.CONTENT_TYPE_NAME },
    Description = "Default pricing list page template",
    IconClass = "xp-list")]

namespace PageTemplates.Pricinglist
{
    public class PricinglistPageTemplateProperties : IPageTemplateProperties
    {
    }
}
