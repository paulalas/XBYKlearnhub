using Kentico.PageBuilder.Web.Mvc.PageTemplates;

[assembly: RegisterPageTemplate(
    identifier: "LearnHub.Price_Default",
    name: "Price Page - Default",
    propertiesType: typeof(PageTemplates.Price.PricePageTemplateProperties),
    customViewName: "~/PageTemplates/Price/_Price.cshtml",
    ContentTypeNames = new[] { LearnHub.Price.CONTENT_TYPE_NAME },
    Description = "Default price page template",
    IconClass = "xp-tag")]

namespace PageTemplates.Price
{
    public class PricePageTemplateProperties : IPageTemplateProperties
    {
    }
}
