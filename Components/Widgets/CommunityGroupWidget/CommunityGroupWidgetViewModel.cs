using System.Collections.Generic;

namespace LearnHub.Widgets
{
    /// <summary>
    /// View model for the Community Group widget.
    /// </summary>
    public class CommunityGroupWidgetViewModel
    {
        public string Title { get; set; }
        public string Heading { get; set; }
        public string Description { get; set; }
        public string ButtonText { get; set; }
        public string ButtonUrl { get; set; }
        public List<string> AvatarImageUrls { get; set; } = new List<string>();
    }
}
