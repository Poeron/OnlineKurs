using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineKurs.Services;
using OnlineKurs.Shared.Models;

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

        public async Task<IActionResult> EditCourse(int id)
        {
            var jwtToken = HttpContext.Session.GetString("jwtToken");
            if (string.IsNullOrEmpty(jwtToken) || _jwtService.GetUserRoleFromToken(jwtToken) != "teacher")
            {
                TempData["Error"] = "You are not authorized to edit this course.";
                return RedirectToAction("Login", "Account");
            }

            var teacherId = int.Parse(_jwtService.GetUserIdFromToken(jwtToken));
            var course = await _courseService.GetCourseByIdAsync(id);

            if (course == null || course.UserId != teacherId)
            {
                TempData["Error"] = "You can only edit your own courses.";
                return RedirectToAction("Home");
            }

            return View(course);
        }

        // POST: Edit Course
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCourse(Courses course)
        {
            var jwtToken = HttpContext.Session.GetString("jwtToken");
            if (string.IsNullOrEmpty(jwtToken) || _jwtService.GetUserRoleFromToken(jwtToken) != "teacher")
            {
                TempData["Error"] = "You are not authorized to edit this course.";
                return RedirectToAction("Login", "Account");
            }

            if (!ModelState.IsValid)
            {
                return View(course);
            }

            var teacherId = int.Parse(_jwtService.GetUserIdFromToken(jwtToken));
            var existingCourse = await _courseService.GetCourseByIdAsync(course.Id);

            if (existingCourse == null || existingCourse.UserId != teacherId)
            {
                TempData["Error"] = "You can only edit your own courses.";
                return RedirectToAction("Home");
            }

            existingCourse.Title = course.Title;
            existingCourse.Description = course.Description;

            await _courseService.UpdateCourseAsync(existingCourse);

            TempData["Success"] = "Course updated successfully.";
            return RedirectToAction("Home");
        }

        // GET: Delete Confirmation
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var jwtToken = HttpContext.Session.GetString("jwtToken");
            if (string.IsNullOrEmpty(jwtToken) || _jwtService.GetUserRoleFromToken(jwtToken) != "teacher")
            {
                TempData["Error"] = "You are not authorized to delete this course.";
                return RedirectToAction("Login", "Account");
            }

            var teacherId = int.Parse(_jwtService.GetUserIdFromToken(jwtToken));
            var course = await _courseService.GetCourseByIdAsync(id);

            if (course == null || course.UserId != teacherId)
            {
                TempData["Error"] = "You can only delete your own courses.";
                return RedirectToAction("Home");
            }

            return View(course);
        }

        // POST: Delete Course
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var jwtToken = HttpContext.Session.GetString("jwtToken");
            if (string.IsNullOrEmpty(jwtToken) || _jwtService.GetUserRoleFromToken(jwtToken) != "teacher")
            {
                TempData["Error"] = "You are not authorized to delete this course.";
                return RedirectToAction("Login", "Account");
            }

            var teacherId = int.Parse(_jwtService.GetUserIdFromToken(jwtToken));
            var course = await _courseService.GetCourseByIdAsync(id);

            if (course == null || course.UserId != teacherId)
            {
                TempData["Error"] = "You can only delete your own courses.";
                return RedirectToAction("Home");
            }

            await _courseService.DeleteCourseAsync(id);

            TempData["Success"] = "Course deleted successfully.";
            return RedirectToAction("Home");
        }


        public IActionResult CreateCourse()
        {
            var jwtToken = HttpContext.Session.GetString("jwtToken");
            if (string.IsNullOrEmpty(jwtToken) || _jwtService.GetUserRoleFromToken(jwtToken) != "teacher")
            {
                TempData["Error"] = "You are not authorized to create a course.";
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCourse(Courses course)
        {
            var jwtToken = HttpContext.Session.GetString("jwtToken");
            if (string.IsNullOrEmpty(jwtToken) || _jwtService.GetUserRoleFromToken(jwtToken) != "teacher")
            {
                TempData["Error"] = "You are not authorized to create a course.";
                return RedirectToAction("Login", "Account");
            }

            if (!ModelState.IsValid)
            {
                return View(course);
            }

            var teacherId = int.Parse(_jwtService.GetUserIdFromToken(jwtToken));
            course.UserId = teacherId;

            await _courseService.AddCourseAsync(course);

            TempData["Success"] = "Course created successfully.";
            return RedirectToAction("Home");
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
