using System.Linq;
using System.Threading.Tasks;
using CMS.ContentEngine;
using Kentico.PageBuilder.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

[assembly: RegisterWidget(
    identifier: "LearnHub.HomeBannerWidget",
    viewComponentType: typeof(LearnHub.Widgets.HomeBannerWidgetViewComponent),
    name: "Home Banner",
    propertiesType: typeof(LearnHub.Widgets.HomeBannerWidgetProperties),
    Description = "Hero banner section with heading, CTA buttons, statistics, and hero image",
    IconClass = "xp-layout")]

namespace LearnHub.Widgets
{
    /// <summary>
    /// Home Banner widget view component.
    /// </summary>
    public class HomeBannerWidgetViewComponent : ViewComponent
    {
        private readonly IContentQueryExecutor _contentQueryExecutor;

        public HomeBannerWidgetViewComponent(IContentQueryExecutor contentQueryExecutor)
        {
            _contentQueryExecutor = contentQueryExecutor;
        }

        /// <summary>
        /// Executes the view component and returns the widget view with populated data.
        /// </summary>
        /// <param name="properties">Widget properties configured in the page builder.</param>
        public async Task<ViewViewComponentResult> InvokeAsync(HomeBannerWidgetProperties properties)
        {
            string heroImageUrl = null;

            // Get the hero image from selected content item
            var heroImageReference = properties.HeroImage?.FirstOrDefault();
            if (heroImageReference != null)
            {
                var queryBuilder = new ContentItemQueryBuilder()
                    .ForContentType(LearnHub.Assets.CONTENT_TYPE_NAME,
                        config => config
                            .Where(where => where.WhereEquals(nameof(ContentItemFields.ContentItemGUID), heroImageReference.Identifier))
                            .TopN(1));

                var assets = await _contentQueryExecutor.GetMappedResult<LearnHub.Assets>(queryBuilder);
                var asset = assets.FirstOrDefault();
                heroImageUrl = asset?.Photo?.Url;
            }

            var viewModel = new HomeBannerWidgetViewModel
            {
                BadgeText = properties.BadgeText,
                MainHeading = properties.MainHeading,
                SubText = properties.SubText,
                CTAText = properties.CTAText,
                CTATargetURL = properties.CTATargetURL,
                YouTubeText = properties.YouTubeText,
                YouTubeURL = properties.YouTubeURL,
                StudentsText = properties.StudentsText,
                CoursesText = properties.CoursesText,
                RatingText = properties.RatingText,
                HeroImageUrl = heroImageUrl,
                VideoURL = properties.VideoURL,
                JoinStudentsText = properties.JoinStudentsText,
                AvailableCoursesText = properties.AvailableCoursesText
            };

            return View("~/Components/Widgets/HomeBannerWidget/_HomeBannerWidget.cshtml", viewModel);
        }
    }
}
