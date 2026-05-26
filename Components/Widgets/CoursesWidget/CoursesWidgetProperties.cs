using System.Collections.Generic;
using Kentico.PageBuilder.Web.Mvc;
using Kentico.Xperience.Admin.Base.FormAnnotations;

namespace LearnHub.Widgets
{
    /// <summary>
    /// Courses widget properties.
    /// </summary>
    public class CoursesWidgetProperties : IWidgetProperties
    {
        /// <summary>
        /// Title text (e.g., "Course").
        /// </summary>
        [TextInputComponent(
            Label = "Title",
            Order = 1)]
        public string Title { get; set; } = "Course";

        /// <summary>
        /// Subtitle heading.
        /// </summary>
        [TextInputComponent(
            Label = "Subtitle",
            Order = 2)]
        public string Subtitle { get; set; } = "Explore Our Popular Courses";

        /// <summary>
        /// Description text.
        /// </summary>
        [TextAreaComponent(
            Label = "Description",
            Order = 3)]
        public string Description { get; set; } = "Choose from hundreds of courses designed by industry experts to help you achieve your goals.";

        /// <summary>
        /// Number of courses to display.
        /// </summary>
        [NumberInputComponent(
            Label = "Number of Courses",
            Order = 4)]
        public int NumberOfCourses { get; set; } = 4;

        /// <summary>
        /// Field to order courses by.
        /// </summary>
        [DropDownComponent(
            Label = "Order By",
            Order = 5,
            Options = "title;Course Title\nprice;Price (Low to High)\nprice_desc;Price (High to Low)\nstars;Rating (Highest First)\nfollowers;Most Popular")]
        public string OrderBy { get; set; } = "title";

        /// <summary>
        /// Show view all button.
        /// </summary>
        [CheckBoxComponent(
            Label = "Show 'View All' Button",
            Order = 6)]
        public bool ShowViewAllButton { get; set; } = true;

        /// <summary>
        /// View all button text.
        /// </summary>
        [TextInputComponent(
            Label = "View All Button Text",
            Order = 7)]
        public string ViewAllButtonText { get; set; } = "View All Courses";

        /// <summary>
        /// View all button URL.
        /// </summary>
        [TextInputComponent(
            Label = "View All Button URL",
            Order = 8)]
        public string ViewAllButtonURL { get; set; }
    }
}
