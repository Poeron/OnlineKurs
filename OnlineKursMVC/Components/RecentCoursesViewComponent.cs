using Microsoft.AspNetCore.Mvc;
using OnlineKurs.Services;
public class RecentCoursesViewComponent : ViewComponent
{
    private readonly CourseService _courseService;

    public RecentCoursesViewComponent(CourseService courseService)
    {
        _courseService = courseService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var courses = await _courseService.GetAllCoursesAsync();
        return View(courses.Take(5)); // Get the 5 most recent courses
    }
}
