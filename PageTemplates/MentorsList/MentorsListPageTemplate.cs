using Kentico.PageBuilder.Web.Mvc.PageTemplates;

[assembly: RegisterPageTemplate(
    identifier: "LearnHub.MentorsList_Default",
    name: "Mentors List Page - Default",
    propertiesType: typeof(PageTemplates.MentorsList.MentorsListPageTemplateProperties),
    customViewName: "~/PageTemplates/MentorsList/_MentorsList.cshtml",
    ContentTypeNames = new[] { LearnHub.MentorsList.CONTENT_TYPE_NAME },
    Description = "Default mentors list page template",
    IconClass = "xp-list")]

namespace PageTemplates.MentorsList
{
    public class MentorsListPageTemplateProperties : IPageTemplateProperties
    {
    }
}
