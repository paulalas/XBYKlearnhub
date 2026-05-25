using Kentico.PageBuilder.Web.Mvc.PageTemplates;

[assembly: RegisterPageTemplate(
    identifier: "LearnHub.Course_Default",
    name: "Course Page - Default",
    propertiesType: typeof(PageTemplates.Course.CoursePageTemplateProperties),
    customViewName: "~/PageTemplates/Course/_Course.cshtml",
    ContentTypeNames = new[] { LearnHub.Course.CONTENT_TYPE_NAME },
    Description = "Default course page template",
    IconClass = "xp-doc")]

namespace PageTemplates.Course
{
    public class CoursePageTemplateProperties : IPageTemplateProperties
    {
    }
}
