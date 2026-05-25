using Kentico.PageBuilder.Web.Mvc.PageTemplates;

[assembly: RegisterPageTemplate(
    identifier: "LearnHub.Testimonial_Default",
    name: "Testimonial Page - Default",
    propertiesType: typeof(PageTemplates.Testimonial.TestimonialPageTemplateProperties),
    customViewName: "~/PageTemplates/Testimonial/_Testimonial.cshtml",
    ContentTypeNames = new[] { LearnHub.Testimonial.CONTENT_TYPE_NAME },
    Description = "Default testimonial page template",
    IconClass = "xp-quote")]

namespace PageTemplates.Testimonial
{
    public class TestimonialPageTemplateProperties : IPageTemplateProperties
    {
    }
}
