using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineKurs.Shared.Models;
using OnlineKurs.Services;

namespace OnlineKurs.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnrollmentsController : ControllerBase
    {
        private readonly EnrollmentService _enrollmentService;

        public EnrollmentsController(EnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }

        [Authorize(Roles = "admin,teacher")]
        [HttpGet]
        public async Task<IActionResult> GetAllEnrollments()
        {
            var enrollments = await _enrollmentService.GetAllEnrollmentsAsync();
            return Ok(enrollments);
        }

        [Authorize(Roles = "admin,teacher,user")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEnrollmentById(int id)
        {
            var enrollment = await _enrollmentService.GetEnrollmentByIdAsync(id);
            if (enrollment == null) return NotFound();
            return Ok(enrollment);
        }

        [Authorize(Roles = "user")]
        [HttpPost]
        public async Task<IActionResult> AddEnrollment([FromBody] Enrollments enrollment)
        {
            await _enrollmentService.AddEnrollmentAsync(enrollment);
            return CreatedAtAction(nameof(GetEnrollmentById), new { id = enrollment.Id }, enrollment);
        }

        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEnrollment(int id, [FromBody] Enrollments enrollment)
        {
            if (id != enrollment.Id) return BadRequest();
            await _enrollmentService.UpdateEnrollmentAsync(enrollment);
            return NoContent();
        }

        [Authorize(Roles = "admin,teacher,user")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEnrollment(int id)
        {
            await _enrollmentService.DeleteEnrollmentAsync(id);
            return NoContent();
        }
    }
}
