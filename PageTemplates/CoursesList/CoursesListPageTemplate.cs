using Kentico.PageBuilder.Web.Mvc.PageTemplates;

[assembly: RegisterPageTemplate(
    identifier: "LearnHub.CoursesList_Default",
    name: "Courses List Page - Default",
    propertiesType: typeof(PageTemplates.CoursesList.CoursesListPageTemplateProperties),
    customViewName: "~/PageTemplates/CoursesList/_CoursesList.cshtml",
    ContentTypeNames = new[] { LearnHub.CoursesList.CONTENT_TYPE_NAME },
    Description = "Default courses list page template",
    IconClass = "xp-list")]

namespace PageTemplates.CoursesList
{
    public class CoursesListPageTemplateProperties : IPageTemplateProperties
    {
    }
}
