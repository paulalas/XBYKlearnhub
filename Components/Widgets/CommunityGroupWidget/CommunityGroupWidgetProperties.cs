using System.Collections.Generic;
using System.Linq;
using Kentico.PageBuilder.Web.Mvc;
using Kentico.Xperience.Admin.Base.FormAnnotations;
using CMS.ContentEngine;

namespace LearnHub.Widgets
{
    /// <summary>
    /// Community Group widget properties.
    /// </summary>
    public class CommunityGroupWidgetProperties : IWidgetProperties
    {
        /// <summary>
        /// Title text above the heading.
        /// </summary>
        [TextInputComponent(
            Label = "Title",
            Order = 1)]
        public string Title { get; set; } = "Community";

        /// <summary>
        /// Main heading text.
        /// </summary>
        [TextInputComponent(
            Label = "Heading",
            Order = 2)]
        public string Heading { get; set; } = "Join Our Thriving <span class=\"text-primary\">Learning Community</span>";

        /// <summary>
        /// Description text.
        /// </summary>
        [TextAreaComponent(
            Label = "Description",
            Order = 3)]
        public string Description { get; set; } = "Learning is better together. Join our community of 50,000+ students and unlock collaborative features that accelerate your growth.";

        /// <summary>
        /// Button text.
        /// </summary>
        [TextInputComponent(
            Label = "Button Text",
            Order = 4)]
        public string ButtonText { get; set; } = "Join Community";

        /// <summary>
        /// Button URL.
        /// </summary>
        [TextInputComponent(
            Label = "Button URL",
            Order = 5)]
        public string ButtonUrl { get; set; } = "#";

        /// <summary>
        /// Avatar images for the community grid (up to 9 images for 3x3 grid).
        /// </summary>
        [ContentItemSelectorComponent(
            LearnHub.Assets.CONTENT_TYPE_NAME,
            Label = "Avatar Images",
            Order = 6,
            MaximumItems = 9)]
        public IEnumerable<ContentItemReference> AvatarImages { get; set; } = Enumerable.Empty<ContentItemReference>();
    }
}
