using Microsoft.AspNetCore.Mvc;
using OnlineKurs.Shared.Models;
using OnlineKurs.Services;

namespace OnlineKursMVC.Controllers
{
	public class UserController : BaseController
	{
        private readonly EnrollmentService _enrollmentService;
        private readonly JwtService _jwtService;

        public UserController(EnrollmentService enrollmentService, JwtService jwtService)
            : base(jwtService)
        {
            _enrollmentService = enrollmentService;
        }
        public async Task<IActionResult> Enrollments()
        {
            if (!IsUser())
            {
                TempData["Error"] = "You are not authorized to access this page.";
                return RedirectToAction("Login", "Account");
            }

            var jwtToken = HttpContext.Session.GetString("jwtToken");
            var userId = int.Parse(_jwtService.GetUserIdFromToken(jwtToken));

            var enrollments = await _enrollmentService.GetEnrollmentsByUserIdAsync(userId);
            return View(enrollments);
        }
    }
}
