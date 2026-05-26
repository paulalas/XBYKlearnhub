using Kentico.PageBuilder.Web.Mvc.PageTemplates;
using LearnHub;

[assembly: RegisterPageTemplate(
    identifier: "LearnHub.Course_Default",
    name: "Course Page - Default",
    propertiesType: typeof(PageTemplates.Course.CoursePageTemplateProperties),
    customViewName: "~/PageTemplates/Course/_Course.cshtml",
    ContentTypeNames = new[] { Course.CONTENT_TYPE_NAME },
    Description = "Default course page template with course details",
    IconClass = "xp-doc")]

namespace PageTemplates.Course
{
    public class CoursePageTemplateProperties : IPageTemplateProperties
    {
        // No additional properties needed - we'll use the page data directly
    }
}
