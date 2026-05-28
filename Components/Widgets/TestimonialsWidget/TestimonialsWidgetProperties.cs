using System.Collections.Generic;
using System.Threading.Tasks;
using Kentico.PageBuilder.Web.Mvc;
using Kentico.Xperience.Admin.Base.FormAnnotations;

namespace LearnHub.Widgets
{
    /// <summary>
    /// Testimonials widget properties.
    /// </summary>
    public class TestimonialsWidgetProperties : IWidgetProperties
    {
        /// <summary>
        /// Number of testimonials to display.
        /// </summary>
        [NumberInputComponent(
            Label = "Number of Testimonials",
            Order = 1)]
        public int NumberOfTestimonials { get; set; } = 6;

        /// <summary>
        /// Order by option for testimonials.
        /// </summary>
        [DropDownComponent(
            Label = "Order By",
            Order = 2,
            DataProviderType = typeof(TestimonialsWidgetOrderByOptionsProvider),
            Options = ";stars_desc;name_asc")]
        public string OrderBy { get; set; } = "stars_desc";
    }

    /// <summary>
    /// Provides ordering options for testimonials widget.
    /// </summary>
    public class TestimonialsWidgetOrderByOptionsProvider : IDropDownOptionsProvider
    {
        public Task<IEnumerable<DropDownOptionItem>> GetOptionItems()
        {
            var options = new List<DropDownOptionItem>
            {
                new DropDownOptionItem { Value = "stars_desc", Text = "Highest Stars" },
                new DropDownOptionItem { Value = "name_asc", Text = "Name (A-Z)" }
            };

            return Task.FromResult<IEnumerable<DropDownOptionItem>>(options);
        }
    }
}
