using Kentico.Content.Web.Mvc.Routing;
using Kentico.PageBuilder.Web.Mvc;
using Kentico.Web.Mvc;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);


// Enable desired Kentico Xperience features
builder.Services.AddKentico(features =>
{
        features.UsePageBuilder(new PageBuilderOptions
    {
        ContentTypeNames = new[]
        {
            LearnHub.Home.CONTENT_TYPE_NAME,
            LearnHub.Course.CONTENT_TYPE_NAME,
            LearnHub.CoursesList.CONTENT_TYPE_NAME,
            LearnHub.Group.CONTENT_TYPE_NAME,
            LearnHub.Mentor.CONTENT_TYPE_NAME,
            LearnHub.MentorsList.CONTENT_TYPE_NAME,
            LearnHub.Price.CONTENT_TYPE_NAME,
            LearnHub.TestimonialsList.CONTENT_TYPE_NAME,
            LearnHub.Testimonial.CONTENT_TYPE_NAME,
            LearnHub.Pricinglist.CONTENT_TYPE_NAME
        }
    });
    // features.UseActivityTracking();
    features.UseWebPageRouting();
    // features.UseEmailStatisticsLogging();
    // features.UseEmailMarketing();
});

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

// Register application services
builder.Services.AddScoped<LearnHub.Services.INavigationService, LearnHub.Services.NavigationService>();

builder.Services.AddControllersWithViews();

var app = builder.Build();
app.InitKentico();

app.UseStaticFiles();

app.UseCookiePolicy();

app.UseAuthentication();


app.UseKentico();

app.UseAuthorization();

app.Kentico().MapRoutes();

app.Run();
