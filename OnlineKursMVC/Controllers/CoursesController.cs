using Microsoft.AspNetCore.Mvc;
using OnlineKurs.Services;
using OnlineKurs.Shared.Models;
namespace OnlineKursMVC.Controllers
{
    public class CoursesController : Controller
    {
        private readonly CourseService _courseService;
        private readonly EnrollmentService _enrollmentService;
        private readonly ReviewService _reviewService;
        private readonly JwtService _jwtService;

        public CoursesController(CourseService courseService, EnrollmentService enrollmentService, ReviewService reviewService, JwtService jwtService)
        {
            _courseService = courseService;
            _enrollmentService = enrollmentService;
            _reviewService = reviewService;
            _jwtService = jwtService;
        }
        [HttpPost]
        public async Task<IActionResult> Leave(int courseId)
        {
            var jwtToken = HttpContext.Session.GetString("jwtToken");
            if (string.IsNullOrEmpty(jwtToken))
            {
                TempData["Error"] = "You must be logged in to leave a course.";
                return RedirectToAction("Details", new { id = courseId });
            }

            var userId = int.Parse(_jwtService.GetUserIdFromToken(jwtToken));

            await _enrollmentService.UnenrollUserAsync(userId, courseId);
            TempData["Success"] = "You have successfully left the course.";
            return RedirectToAction("Details", new { id = courseId });
        }
        public async Task<IActionResult> Details(int id)
        {
            var jwtToken = HttpContext.Session.GetString("jwtToken");
            if (string.IsNullOrEmpty(jwtToken))
            {
                ViewBag.IsEnrolled = false;
                ViewBag.UserReview = null;
                ViewBag.AllReviews = null;
                return View(await _courseService.GetCourseByIdAsync(id));
            }
            var userId = int.Parse(_jwtService.GetUserIdFromToken(jwtToken));

            var course = await _courseService.GetCourseByIdAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            var isEnrolled = userId != null && await _enrollmentService.IsUserEnrolledAsync(userId, id);
            var userReview = userId != null && isEnrolled
                ? (await _reviewService.GetReviewsByUserIdAsync(userId)).FirstOrDefault(r => r.CourseId == id)
                : null;

            var allReviews = await _reviewService.GetReviewsByCourseIdAsync(id);

            ViewBag.IsEnrolled = isEnrolled;
            ViewBag.UserReview = userReview;
            ViewBag.AllReviews = allReviews;

            return View(course);
        }



        [HttpPost]
        public async Task<IActionResult> AddReview(int courseId, int rating, string comment)
        {
            var jwtToken = HttpContext.Session.GetString("jwtToken");
            if (string.IsNullOrEmpty(jwtToken))
            {
                TempData["Error"] = "You must be logged in to submit a review.";
                return RedirectToAction("Details", new { id = courseId });
            }

            var userId = int.Parse(_jwtService.GetUserIdFromToken(jwtToken));

            var existingReview = (await _reviewService.GetReviewsByUserIdAsync(userId))
                .FirstOrDefault(r => r.CourseId == courseId);

            if (existingReview != null)
            {
                existingReview.Rating = rating;
                existingReview.Comment = comment;
                await _reviewService.UpdateReviewAsync(existingReview);
                TempData["Success"] = "Your review has been updated.";
            }
            else
            {
                var review = new Reviews
                {
                    CourseId = courseId,
                    UserId = userId,
                    Rating = rating,
                    Comment = comment,
                    CreatedAt = DateTime.UtcNow
                };
                await _reviewService.AddReviewAsync(review);
                TempData["Success"] = "Your review has been submitted.";
            }

            return RedirectToAction("Details", new { id = courseId });
        }

        [HttpPost]
        public async Task<IActionResult> Enroll(int courseId)
        {
            var token = HttpContext.Session.GetString("jwtToken");

            if (string.IsNullOrEmpty(token))
            {
                TempData["Error"] = "You need to login to enroll in a course.";
                return RedirectToAction("Details", new { id = courseId });
            }
            var userId = int.Parse(_jwtService.GetUserIdFromToken(token));

            if (await _enrollmentService.IsUserEnrolledAsync(userId, courseId))
            {
                TempData["Error"] = "You are already enrolled in this course.";
                return RedirectToAction("Details", new { id = courseId });
            }
            await _enrollmentService.EnrollUserAsync(userId,courseId);
            TempData["Success"] = "You have successfully enrolled in the course!";
            return RedirectToAction("Details", new { id = courseId });
        }


        public IActionResult Index()
        {
            var courses = _courseService.GetAllCoursesAsync().Result;
            return View(courses);
        }
	}
}
