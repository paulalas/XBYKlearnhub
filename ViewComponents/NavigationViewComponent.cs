using System.Threading.Tasks;
using LearnHub.Services;
using Microsoft.AspNetCore.Mvc;

namespace LearnHub.ViewComponents
{
    [ViewComponent(Name = "Navigation")]
    public class NavigationViewComponent : ViewComponent
    {
        private readonly INavigationService navigationService;

        public NavigationViewComponent(INavigationService navigationService)
        {
            this.navigationService = navigationService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var navigationItems = await navigationService.GetNavigationItemsAsync(HttpContext.RequestAborted);
            return View("~/Views/Shared/Navigation/_Navigation.cshtml", navigationItems);
        }
    }
}
