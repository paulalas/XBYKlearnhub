using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS.ContentEngine;
using Kentico.PageBuilder.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

[assembly: RegisterWidget(
    identifier: "LearnHub.CommunityGroupWidget",
    viewComponentType: typeof(LearnHub.Widgets.CommunityGroupWidgetViewComponent),
    name: "Community Group Widget",
    propertiesType: typeof(LearnHub.Widgets.CommunityGroupWidgetProperties),
    Description = "Displays community features with avatar grid",
    IconClass = "xp-users")]

namespace LearnHub.Widgets
{
    /// <summary>
    /// Community Group widget view component.
    /// </summary>
    public class CommunityGroupWidgetViewComponent : ViewComponent
    {
        private readonly IContentQueryExecutor _contentQueryExecutor;

        public CommunityGroupWidgetViewComponent(IContentQueryExecutor contentQueryExecutor)
        {
            _contentQueryExecutor = contentQueryExecutor;
        }

        /// <summary>
        /// Executes the view component and returns the widget view with populated data.
        /// </summary>
        public async Task<ViewViewComponentResult> InvokeAsync(CommunityGroupWidgetProperties properties)
        {
            var avatarUrls = new List<string>();

            // Get avatar images from selected content items
            if (properties.AvatarImages != null && properties.AvatarImages.Any())
            {
                foreach (var avatarReference in properties.AvatarImages)
                {
                    var queryBuilder = new ContentItemQueryBuilder()
                        .ForContentType(LearnHub.Assets.CONTENT_TYPE_NAME,
                            config => config
                                .Where(where => where.WhereEquals(nameof(ContentItemFields.ContentItemGUID), avatarReference.Identifier))
                                .TopN(1));

                    var assets = await _contentQueryExecutor.GetMappedResult<LearnHub.Assets>(queryBuilder);
                    var asset = assets.FirstOrDefault();
                    
                    if (asset?.Photo?.Url != null)
                    {
                        avatarUrls.Add(asset.Photo.Url);
                    }
                }
            }

            var viewModel = new CommunityGroupWidgetViewModel
            {
                Title = properties.Title,
                Heading = properties.Heading,
                Description = properties.Description,
                ButtonText = properties.ButtonText,
                ButtonUrl = properties.ButtonUrl,
                AvatarImageUrls = avatarUrls
            };

            return View("~/Components/Widgets/CommunityGroupWidget/_CommunityGroupWidget.cshtml", viewModel);
        }
    }
}
