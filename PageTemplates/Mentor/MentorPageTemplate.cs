using Kentico.PageBuilder.Web.Mvc.PageTemplates;

[assembly: RegisterPageTemplate(
    identifier: "LearnHub.Mentor_Default",
    name: "Mentor Page - Default",
    propertiesType: typeof(PageTemplates.Mentor.MentorPageTemplateProperties),
    customViewName: "~/PageTemplates/Mentor/_Mentor.cshtml",
    ContentTypeNames = new[] { LearnHub.Mentor.CONTENT_TYPE_NAME },
    Description = "Default mentor page template",
    IconClass = "xp-user")]

namespace PageTemplates.Mentor
{
    public class MentorPageTemplateProperties : IPageTemplateProperties
    {
    }
}
