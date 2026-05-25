using Kentico.PageBuilder.Web.Mvc.PageTemplates;

[assembly: RegisterPageTemplate(
    identifier: "LearnHub.TestimonialsList_Default",
    name: "Testimonials List Page - Default",
    propertiesType: typeof(PageTemplates.TestimonialsList.TestimonialsListPageTemplateProperties),
    customViewName: "~/PageTemplates/TestimonialsList/_TestimonialsList.cshtml",
    ContentTypeNames = new[] { LearnHub.TestimonialsList.CONTENT_TYPE_NAME },
    Description = "Default testimonials list page template",
    IconClass = "xp-list")]

namespace PageTemplates.TestimonialsList
{
    public class TestimonialsListPageTemplateProperties : IPageTemplateProperties
    {
    }
}
