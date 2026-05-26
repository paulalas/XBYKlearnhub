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
    identifier: "LearnHub.MentorsWidget",
    viewComponentType: typeof(LearnHub.Widgets.MentorsWidgetViewComponent),
    name: "Mentors Widget",
    propertiesType: typeof(LearnHub.Widgets.MentorsWidgetProperties),
    Description = "Displays a grid of mentors with configurable ordering options",
    IconClass = "xp-user")]

namespace LearnHub.Widgets
{
    /// <summary>
    /// Mentors widget view component.
    /// </summary>
    public class MentorsWidgetViewComponent : ViewComponent
    {
        private readonly IContentRetriever _contentRetriever;

        public MentorsWidgetViewComponent(
            IContentRetriever contentRetriever)
        {
            _contentRetriever = contentRetriever;
        }

        /// <summary>
        /// Executes the view component and returns the widget view with populated data.
        /// </summary>
        public async Task<ViewViewComponentResult> InvokeAsync(MentorsWidgetProperties properties)
        {
            // Build the ordering columns based on the selected option
            var orderByColumns = GetOrderByColumns(properties.OrderBy);

            // Retrieve mentors with linked items (Assets for thumbnails)
            var mentors = await _contentRetriever.RetrievePages<Mentor>(
                new RetrievePagesParameters
                {
                    LinkedItemsMaxLevel = 1  // Load linked Assets for MentorThumbnail
                },
                query => query
                    .OrderBy(orderByColumns)
                    .TopN(properties.NumberOfMentors),
                new RetrievalCacheSettings($"MentorsWidget_{properties.OrderBy}_{properties.NumberOfMentors}", TimeSpan.FromMinutes(5)),
                HttpContext.RequestAborted
            );

            // Map mentors to view model
            var mentorViewModels = new List<MentorCardViewModel>();
            foreach (var mentor in mentors)
            {
                // Get thumbnail URL - Assets are now loaded because LinkedItemsMaxLevel = 1
                string thumbnailUrl = null;
                var thumbnail = mentor.MentorThumbnail?.FirstOrDefault();
                if (thumbnail != null)
                {
                    thumbnailUrl = thumbnail.Photo?.Url;
                }

                mentorViewModels.Add(new MentorCardViewModel
                {
                    Name = mentor.MentorName,
                    Title = mentor.MentorTitle,
                    Description = mentor.MentorDescription,
                    Followers = mentor.MentorFollowers,
                    Courses = mentor.MentorCourses,
                    Stars = mentor.MentorStars,
                    ThumbnailUrl = thumbnailUrl,
                    FirstLinkIcon = mentor.FirstLinkIcon,
                    FirstButtonLink = mentor.FirstButtonLink ?? "#",
                    SecondLinkIcon = mentor.SecondLinkIcon,
                    SecondButtonLink = mentor.SecondButtonLink ?? "#",
                    ThirdLinkIcon = mentor.ThirdLinkIcon,
                    ThirdButtonLink = mentor.ThirdButtonLink ?? "#"
                });
            }

            var viewModel = new MentorsWidgetViewModel
            {
                Mentors = mentorViewModels
            };

            return View("~/Components/Widgets/MentorsWidget/_MentorsWidget.cshtml", viewModel);
        }

        /// <summary>
        /// Gets the ordering columns based on the selected order by option.
        /// </summary>
        private static OrderByColumn[] GetOrderByColumns(string orderBy)
        {
            return orderBy switch
            {
                "stars_desc" => new[] { new OrderByColumn(nameof(Mentor.MentorStars), OrderDirection.Descending) },
                "followers_desc" => new[] { new OrderByColumn(nameof(Mentor.MentorFollowers), OrderDirection.Descending) },
                "courses_desc" => new[] { new OrderByColumn(nameof(Mentor.MentorCourses), OrderDirection.Descending) },
                "name_asc" => new[] { new OrderByColumn(nameof(Mentor.MentorName), OrderDirection.Ascending) },
                _ => new[] { new OrderByColumn(nameof(Mentor.MentorStars), OrderDirection.Descending) }
            };
        }
    }
}
