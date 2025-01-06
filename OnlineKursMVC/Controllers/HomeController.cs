using Microsoft.AspNetCore.Mvc;
using OnlineKurs.Services;

namespace OnlineKursMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly CourseService _courseService;

        public HomeController(CourseService courseService)
        {
            _courseService = courseService;
        }

        public IActionResult Index()
        {
            // Fetch recent courses to display on the home page
            var recentCourses = _courseService.GetAllCoursesAsync().Result.Take(5).ToList();
            return View(recentCourses);
        }
    }
}
