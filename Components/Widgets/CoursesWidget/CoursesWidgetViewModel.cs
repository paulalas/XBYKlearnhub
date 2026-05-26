using System.Collections.Generic;

namespace LearnHub.Widgets
{
    /// <summary>
    /// View model for the Courses widget.
    /// </summary>
    public class CoursesWidgetViewModel
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Description { get; set; }
        public bool ShowViewAllButton { get; set; }
        public string ViewAllButtonText { get; set; }
        public string ViewAllButtonURL { get; set; }
        public List<CourseCardViewModel> Courses { get; set; } = new List<CourseCardViewModel>();
    }

    /// <summary>
    /// View model for individual course card.
    /// </summary>
    public class CourseCardViewModel
    {
        public string Title { get; set; }
        public string TeacherName { get; set; }
        public string Duration { get; set; }
        public decimal Followers { get; set; }
        public decimal Stars { get; set; }
        public decimal Price { get; set; }
        public string ThumbnailUrl { get; set; }
        public string ButtonText { get; set; }
        public string ButtonUrl { get; set; }
    }
}
