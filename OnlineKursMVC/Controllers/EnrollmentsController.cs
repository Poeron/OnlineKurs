using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;
using OnlineKurs.Shared.Models;
using OnlineKurs.Services;
using System.IdentityModel.Tokens.Jwt;

namespace OnlineKursMVC.Controllers
{
    public class EnrollmentsController : Controller
    {
        private readonly EnrollmentService _enrollmentService;
        private readonly CourseService _courseService;
        private readonly JwtService _jwtService;

        public EnrollmentsController(EnrollmentService enrollmentService, CourseService courseService, JwtService jwtService)
        {
            _enrollmentService = enrollmentService;
            _courseService = courseService;
            _jwtService = jwtService;
        }

        public async Task<IActionResult> Index()
        {
            var jwtToken = HttpContext.Session.GetString("jwtToken");
            if (string.IsNullOrEmpty(jwtToken))
            {
                TempData["Error"] = "You must be logged in to view your enrollments.";
                return RedirectToAction("Login", "Account");
            }
            var userId = _jwtService.GetUserIdFromToken(jwtToken);
            if (string.IsNullOrEmpty(userId))
            {
                TempData["Error"] = "Invalid user information.";
                return RedirectToAction("Login", "Account");
            }

            // Fetch enrolled courses
            var enrollments = await _enrollmentService.GetEnrollmentsByUserIdAsync(int.Parse(userId));
            return View(enrollments);
        }
    }
}
