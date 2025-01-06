using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineKurs.Shared.Models;
using OnlineKurs.Services;

namespace OnlineKurs.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly CourseService _courseService;

        public CoursesController(CourseService courseService)
        {
            _courseService = courseService;
        }

        [Authorize(Roles = "admin,user,teacher")]
        [HttpGet]
        public async Task<IActionResult> GetAllCourses()
        {
            var courses = await _courseService.GetAllCoursesAsync();
            return Ok(courses);
        }

        [Authorize(Roles = "admin,teacher,user")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourseById(int id)
        {
            var course = await _courseService.GetCourseByIdAsync(id);
            if (course == null) return NotFound();
            return Ok(course);
        }

        [Authorize(Roles = "admin,teacher")]
        [HttpPost]
        public async Task<IActionResult> AddCourse([FromBody] Courses course)
        {
            await _courseService.AddCourseAsync(course);
            return CreatedAtAction(nameof(GetCourseById), new { id = course.Id }, course);
        }

        [Authorize(Roles = "admin,teacher")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourse(int id, [FromBody] Courses course)
        {
            if (id != course.Id) return BadRequest();
            await _courseService.UpdateCourseAsync(course);
            return NoContent();
        }

        [Authorize(Roles = "admin,teacher")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            await _courseService.DeleteCourseAsync(id);
            return NoContent();
        }

    }
}
