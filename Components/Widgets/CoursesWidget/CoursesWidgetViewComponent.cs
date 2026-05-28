using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS.ContentEngine;
using CMS.DataEngine;
using CMS.Websites;
using CMS.Websites.Routing;
using Kentico.Content.Web.Mvc;
using Kentico.Content.Web.Mvc.Routing;
using Kentico.PageBuilder.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

[assembly: RegisterWidget(
    identifier: "LearnHub.CoursesWidget",
    viewComponentType: typeof(LearnHub.Widgets.CoursesWidgetViewComponent),
    name: "Courses Widget",
    propertiesType: typeof(LearnHub.Widgets.CoursesWidgetProperties),
    Description = "Displays a grid of courses with configurable header and sorting options",
    IconClass = "xp-list")]

namespace LearnHub.Widgets
{
    /// <summary>
    /// Courses widget view component.
    /// </summary>
    public class CoursesWidgetViewComponent : ViewComponent
    {
        private readonly IContentRetriever _contentRetriever;
        private readonly IWebPageUrlRetriever _webPageUrlRetriever;
        private readonly IWebsiteChannelContext _websiteChannelContext;
        private readonly IPreferredLanguageRetriever _preferredLanguageRetriever;

        public CoursesWidgetViewComponent(
            IContentRetriever contentRetriever,
            IWebPageUrlRetriever webPageUrlRetriever,
            IWebsiteChannelContext websiteChannelContext,
            IPreferredLanguageRetriever preferredLanguageRetriever)
        {
            _contentRetriever = contentRetriever;
            _webPageUrlRetriever = webPageUrlRetriever;
            _websiteChannelContext = websiteChannelContext;
            _preferredLanguageRetriever = preferredLanguageRetriever;
        }

        /// <summary>
        /// Executes the view component and returns the widget view with populated data.
        /// </summary>
        public async Task<ViewViewComponentResult> InvokeAsync(CoursesWidgetProperties properties)
        {
            // Build the ordering columns based on the selected option
            var orderByColumns = GetOrderByColumns(properties.OrderBy);

            // Retrieve courses with linked items (Assets for thumbnails)
            var courses = await _contentRetriever.RetrievePages<Course>(
                new RetrievePagesParameters
                {
                    LinkedItemsMaxLevel = 1  // Load linked Assets for CourseThumbnail
                },
                query => query
                    .OrderBy(orderByColumns)
                    .TopN(properties.NumberOfCourses),
                new RetrievalCacheSettings($"CoursesWidget_{properties.OrderBy}_{properties.NumberOfCourses}", TimeSpan.FromMinutes(5)),
                HttpContext.RequestAborted
            );

            if (!courses.Any())
            {
                return View("~/Components/Widgets/CoursesWidget/_CoursesWidget.cshtml", new CoursesWidgetViewModel
                {
                    Title = properties.Title,
                    Subtitle = properties.Subtitle,
                    Description = properties.Description,
                    ShowViewAllButton = properties.ShowViewAllButton,
                    ViewAllButtonText = properties.ViewAllButtonText,
                    ViewAllButtonURL = properties.ViewAllButtonURL,
                    Courses = new List<CourseCardViewModel>()
                });
            }

            // Get current language
            var languageName = _preferredLanguageRetriever.Get();

            // Collect all WebPageGuids from ButtonLink
            var linkedPageGuids = courses
                .Where(x => x.ButtonLink?.Any() == true)
                .SelectMany(x => x.ButtonLink.Select(y => y.WebPageGuid))
                .Distinct()
                .ToList();

            // Bulk retrieve URLs for all linked pages
            var urls = linkedPageGuids.Any()
                ? await _webPageUrlRetriever.Retrieve(
                    [.. linkedPageGuids],
                    _websiteChannelContext.WebsiteChannelName,
                    languageName,
                    _websiteChannelContext.IsPreview,
                    HttpContext.RequestAborted)
                : new Dictionary<System.Guid, WebPageUrl>();

            // Map courses to view model
            var courseViewModels = new List<CourseCardViewModel>();
            foreach (var course in courses)
            {
                // Get thumbnail URL - Assets are now loaded because LinkedItemsMaxLevel = 1
                string thumbnailUrl = null;
                var thumbnail = course.CourseThumbnail?.FirstOrDefault();
                if (thumbnail != null)
                {
                    thumbnailUrl = thumbnail.Photo?.Url;
                }

                // Get button link from ButtonLink property
                string buttonUrl = "#";
                if (course.ButtonLink?.Any() == true)
                {
                    var linkedPageGuid = course.ButtonLink.First().WebPageGuid;
                    if (urls.ContainsKey(linkedPageGuid))
                    {
                        buttonUrl = urls[linkedPageGuid].RelativePath;
                    }
                }

                courseViewModels.Add(new CourseCardViewModel
                {
                    Title = course.CourseTitle,
                    TeacherName = course.CourseTeacherName,
                    Duration = course.CourseDuration,
                    Followers = course.CourseFollowers,
                    Stars = course.CourseStars,
                    Price = course.CoursePrice,
                    ThumbnailUrl = thumbnailUrl,
                    ButtonText = course.ButtonName ?? "Enroll now",
                    ButtonUrl = buttonUrl
                });
            }

            var viewModel = new CoursesWidgetViewModel
            {
                Title = properties.Title,
                Subtitle = properties.Subtitle,
                Description = properties.Description,
                ShowViewAllButton = properties.ShowViewAllButton,
                ViewAllButtonText = properties.ViewAllButtonText,
                ViewAllButtonURL = properties.ViewAllButtonURL,
                Courses = courseViewModels
            };

            return View("~/Components/Widgets/CoursesWidget/_CoursesWidget.cshtml", viewModel);
        }

        /// <summary>
        /// Gets the ordering columns based on the selected order by option.
        /// </summary>
        private static OrderByColumn[] GetOrderByColumns(string orderBy)
        {
            switch (orderBy)
            {
                case "price":
                    return new[] { OrderByColumn.Asc(nameof(Course.CoursePrice)) };
                case "price_desc":
                    return new[] { OrderByColumn.Desc(nameof(Course.CoursePrice)) };
                case "stars":
                    return new[] { OrderByColumn.Desc(nameof(Course.CourseStars)) };
                case "followers":
                    return new[] { OrderByColumn.Desc(nameof(Course.CourseFollowers)) };
                case "title":
                default:
                    return new[] { OrderByColumn.Asc(nameof(Course.CourseTitle)) };
            }
        }
    }
}
