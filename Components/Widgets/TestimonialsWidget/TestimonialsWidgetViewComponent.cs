using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS.ContentEngine;
using CMS.DataEngine;
using Kentico.Content.Web.Mvc;
using Kentico.PageBuilder.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

[assembly: RegisterWidget(
    identifier: "LearnHub.TestimonialsWidget",
    viewComponentType: typeof(LearnHub.Widgets.TestimonialsWidgetViewComponent),
    name: "Testimonials Widget",
    propertiesType: typeof(LearnHub.Widgets.TestimonialsWidgetProperties),
    Description = "Displays testimonial cards in a grid layout",
    IconClass = "xp-comments")]

namespace LearnHub.Widgets
{
    /// <summary>
    /// Testimonials widget view component.
    /// </summary>
    public class TestimonialsWidgetViewComponent : ViewComponent
    {
        private readonly IContentRetriever _contentRetriever;

        public TestimonialsWidgetViewComponent(
            IContentRetriever contentRetriever)
        {
            _contentRetriever = contentRetriever;
        }

        /// <summary>
        /// Executes the view component and returns the widget view with populated data.
        /// </summary>
        public async Task<ViewViewComponentResult> InvokeAsync(TestimonialsWidgetProperties properties)
        {
            // Build the ordering columns based on the selected option
            var orderByColumns = GetOrderByColumns(properties.OrderBy);

            // Retrieve testimonials with linked items (Assets for thumbnails)
            var testimonials = await _contentRetriever.RetrievePages<Testimonial>(
                new RetrievePagesParameters
                {
                    LinkedItemsMaxLevel = 1  // Load linked Assets for TestimonialThumbnail
                },
                query => query
                    .OrderBy(orderByColumns.ToArray())
                    .TopN(properties.NumberOfTestimonials),
                new RetrievalCacheSettings($"TestimonialsWidget_{properties.OrderBy}_{properties.NumberOfTestimonials}", TimeSpan.FromMinutes(5)),
                HttpContext.RequestAborted
            );

            // Map testimonials to view model
            var viewModel = new TestimonialsWidgetViewModel();

            foreach (var testimonial in testimonials)
            {
                // Get thumbnail URL - Assets are now loaded because LinkedItemsMaxLevel = 1
                string thumbnailUrl = null;
                var thumbnail = testimonial.TestimonialThumbnail?.FirstOrDefault();
                if (thumbnail != null)
                {
                    thumbnailUrl = thumbnail.Photo?.Url;
                }

                viewModel.Testimonials.Add(new TestimonialCardViewModel
                {
                    Name = testimonial.TestimonialName,
                    Description = testimonial.TestimonialDescription,
                    JobDescription = testimonial.TestimonialJobDescription,
                    Stars = testimonial.TestimonialStars,
                    ThumbnailUrl = thumbnailUrl,
                    PageUrl = "#"  // Can be enhanced to link to actual testimonial pages
                });
            }

            return View("~/Components/Widgets/TestimonialsWidget/_TestimonialsWidget.cshtml", viewModel);
        }

        /// <summary>
        /// Gets the order by columns based on the selected option.
        /// </summary>
        private static IEnumerable<OrderByColumn> GetOrderByColumns(string orderBy)
        {
            return orderBy switch
            {
                "stars_desc" => new[] { new OrderByColumn(nameof(Testimonial.TestimonialStars), OrderDirection.Descending) },
                "name_asc" => new[] { new OrderByColumn(nameof(Testimonial.TestimonialName), OrderDirection.Ascending) },
                _ => new[] { new OrderByColumn(nameof(Testimonial.TestimonialStars), OrderDirection.Descending) }
            };
        }
    }
}
