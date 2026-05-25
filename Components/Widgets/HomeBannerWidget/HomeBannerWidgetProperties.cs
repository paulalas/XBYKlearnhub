using System.Collections.Generic;
using System.Linq;
using Kentico.PageBuilder.Web.Mvc;
using Kentico.Xperience.Admin.Base.FormAnnotations;
using CMS.ContentEngine;

namespace LearnHub.Widgets
{
    /// <summary>
    /// Home Banner widget properties.
    /// </summary>
    public class HomeBannerWidgetProperties : IWidgetProperties
    {
        /// <summary>
        /// Badge text displayed above the main heading.
        /// </summary>
        [TextInputComponent(
            Label = "Badge Text",
            Order = 1)]
        public string BadgeText { get; set; } = "New Courses Available";

        /// <summary>
        /// Main heading text.
        /// </summary>
        [TextAreaComponent(
            Label = "Main Heading",
            Order = 2)]
        public string MainHeading { get; set; } = "Master New Skills Online Anytime, Anywhere";

        /// <summary>
        /// Subtext/description paragraph.
        /// </summary>
        [TextAreaComponent(
            Label = "Sub Text",
            Order = 3)]
        public string SubText { get; set; } = "Join over 50,000+ students learning from world-class mentors. Transform your career with industry-leading courses.";

        /// <summary>
        /// Primary CTA button text.
        /// </summary>
        [TextInputComponent(
            Label = "CTA Button Text",
            Order = 4)]
        public string CTAText { get; set; } = "Start Learning";

        /// <summary>
        /// Primary CTA button target URL.
        /// </summary>
        [TextInputComponent(
            Label = "CTA Target URL",
            Order = 5)]
        public string CTATargetURL { get; set; }

        /// <summary>
        /// YouTube button text.
        /// </summary>
        [TextInputComponent(
            Label = "YouTube Button Text",
            Order = 6)]
        public string YouTubeText { get; set; } = "YouTube Channel";

        /// <summary>
        /// YouTube channel or video URL.
        /// </summary>
        [TextInputComponent(
            Label = "YouTube URL",
            Order = 7)]
        public string YouTubeURL { get; set; }

        /// <summary>
        /// Students statistic text.
        /// </summary>
        [TextInputComponent(
            Label = "Students Count Text",
            Order = 8)]
        public string StudentsText { get; set; } = "50K+ Students";

        /// <summary>
        /// Courses statistic text.
        /// </summary>
        [TextInputComponent(
            Label = "Courses Count Text",
            Order = 9)]
        public string CoursesText { get; set; } = "200+ Courses";

        /// <summary>
        /// Rating statistic text.
        /// </summary>
        [TextInputComponent(
            Label = "Rating Text",
            Order = 10)]
        public string RatingText { get; set; } = "4.9 Rating";

        /// <summary>
        /// Hero image asset content item.
        /// </summary>
        [ContentItemSelectorComponent(
            LearnHub.Assets.CONTENT_TYPE_NAME,
            Label = "Hero Image",
            Order = 11,
            MaximumItems = 1)]
        public IEnumerable<ContentItemReference> HeroImage { get; set; } = Enumerable.Empty<ContentItemReference>();

        /// <summary>
        /// Video URL for the play button overlay.
        /// </summary>
        [TextInputComponent(
            Label = "Video URL",
            Order = 12)]
        public string VideoURL { get; set; } = "https://www.youtube.com/watch?v=CivuutI6lXY";

        /// <summary>
        /// Text for "Join students" badge on image.
        /// </summary>
        [TextInputComponent(
            Label = "Join Students Text",
            Order = 13)]
        public string JoinStudentsText { get; set; } = "Join 50k+ Students";

        /// <summary>
        /// Text for "Available courses" badge on image.
        /// </summary>
        [TextInputComponent(
            Label = "Available Courses Text",
            Order = 14)]
        public string AvailableCoursesText { get; set; } = "200+ Courses Available Now";
    }
}
