using Kentico.PageBuilder.Web.Mvc.PageTemplates;

[assembly: RegisterPageTemplate(
    identifier: "LearnHub.Home_Default",
    name: "Home Page - Default",
    propertiesType: typeof(PageTemplates.Home.HomePageTemplateProperties),
    customViewName: "~/PageTemplates/Home/_Home.cshtml",
    ContentTypeNames = new[] { LearnHub.Home.CONTENT_TYPE_NAME },
    Description = "Default home page template with editable areas for Page Builder content",
    IconClass = "xp-layout")]

namespace PageTemplates.Home
{
    /// <summary>
    /// Home page template properties.
    /// </summary>
    public class HomePageTemplateProperties : IPageTemplateProperties
    {
        // Add configurable properties here if needed
    }
}
