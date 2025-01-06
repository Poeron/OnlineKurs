using Microsoft.AspNetCore.Mvc;
using OnlineKurs.Services;

namespace OnlineKursMVC.Controllers
{
    public class TeacherController : BaseController
    {
        private readonly CourseService _courseService;
        private readonly JwtService _jwtService;

        public TeacherController(CourseService courseService, JwtService jwtService)
            : base(jwtService)
        {
            _courseService = courseService;
            _jwtService = jwtService;
        }

        public async Task<IActionResult> Home()
        {
            if (!IsTeacher())
            {
                TempData["Error"] = "You are not authorized to access this page.";
                return RedirectToAction("Login", "Account");
            }
            var jwtToken = HttpContext.Session.GetString("jwtToken");
            if (string.IsNullOrEmpty(jwtToken))
            {
                TempData["Error"] = "You must be logged in to access the teacher dashboard.";
                return RedirectToAction("Login", "Account");
            }

            var teacherId = int.Parse(_jwtService.GetUserIdFromToken(jwtToken));
            var courses = await _courseService.GetCoursesByUserIdAsync(teacherId);

            var totalEnrollments = courses.Sum(c => c.Enrollments.Count); // Assuming Enrollments is a navigation property
            var averageRating = courses.SelectMany(c => c.Reviews).DefaultIfEmpty().Average(r => r?.Rating ?? 0);

            ViewBag.TotalEnrollments = totalEnrollments;
            ViewBag.AverageRating = averageRating;

            return View(courses);
        }

    }
}
