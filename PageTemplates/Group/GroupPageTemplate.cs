using Kentico.PageBuilder.Web.Mvc.PageTemplates;

[assembly: RegisterPageTemplate(
    identifier: "LearnHub.Group_Default",
    name: "Group Page - Default",
    propertiesType: typeof(PageTemplates.Group.GroupPageTemplateProperties),
    customViewName: "~/PageTemplates/Group/_Group.cshtml",
    ContentTypeNames = new[] { LearnHub.Group.CONTENT_TYPE_NAME },
    Description = "Default group page template",
    IconClass = "xp-group")]

namespace PageTemplates.Group
{
    public class GroupPageTemplateProperties : IPageTemplateProperties
    {
    }
}
