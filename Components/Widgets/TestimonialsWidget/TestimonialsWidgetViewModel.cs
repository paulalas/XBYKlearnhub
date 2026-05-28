using System.Collections.Generic;

namespace LearnHub.Widgets
{
    /// <summary>
    /// View model for the Testimonials widget.
    /// </summary>
    public class TestimonialsWidgetViewModel
    {
        public List<TestimonialCardViewModel> Testimonials { get; set; } = new List<TestimonialCardViewModel>();
    }

    /// <summary>
    /// View model for individual testimonial card.
    /// </summary>
    public class TestimonialCardViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string JobDescription { get; set; }
        public decimal Stars { get; set; }
        public string ThumbnailUrl { get; set; }
        public string PageUrl { get; set; }
    }
}
