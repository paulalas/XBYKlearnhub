using System.Collections.Generic;

namespace LearnHub.Widgets
{
    /// <summary>
    /// View model for the Mentors widget.
    /// </summary>
    public class MentorsWidgetViewModel
    {
        public List<MentorCardViewModel> Mentors { get; set; } = new List<MentorCardViewModel>();
    }

    /// <summary>
    /// View model for a single mentor card.
    /// </summary>
    public class MentorCardViewModel
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Followers { get; set; }
        public int Courses { get; set; }
        public decimal Stars { get; set; }
        public string ThumbnailUrl { get; set; }
        public string FirstLinkIcon { get; set; }
        public string FirstButtonLink { get; set; }
        public string SecondLinkIcon { get; set; }
        public string SecondButtonLink { get; set; }
        public string ThirdLinkIcon { get; set; }
        public string ThirdButtonLink { get; set; }
    }
}
