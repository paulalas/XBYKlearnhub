using Kentico.PageBuilder.Web.Mvc;
using Kentico.Xperience.Admin.Base.FormAnnotations;

namespace LearnHub.Widgets
{
    /// <summary>
    /// Mentors widget properties.
    /// </summary>
    public class MentorsWidgetProperties : IWidgetProperties
    {
        /// <summary>
        /// Number of mentors to display.
        /// </summary>
        [NumberInputComponent(
            Label = "Number of Mentors",
            Order = 1)]
        public int NumberOfMentors { get; set; } = 4;

        /// <summary>
        /// Order by option for mentors.
        /// </summary>
        [DropDownComponent(
            Label = "Order By",
            Order = 2,
            Options = "stars_desc;Highest Rating\nfollowers_desc;Most Students\ncourses_desc;Most Courses\nname_asc;Name A-Z")]
        public string OrderBy { get; set; } = "stars_desc";
    }
}
