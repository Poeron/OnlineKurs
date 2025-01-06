using OnlineKurs.Shared.Models;
using OnlineKurs.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using OnlineKurs.Data;

namespace OnlineKurs.Repositories
{
    public class EnrollmentRepository : BaseRepository<Enrollments>, IEnrollmentRepository
    {
        public EnrollmentRepository(OnlineKursContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Enrollments>> GetEnrollmentsByCourseIdAsync(int courseId)
        {
            return await _dbSet.Where(e => e.CourseId == courseId).ToListAsync();
        }

        public async Task<IEnumerable<Enrollments>> GetEnrollmentsByUserIdAsync(int userId)
        {
            return await _dbSet
                .Include(e => e.Course) // Ensure Course navigation property is included
                .ThenInclude(c => c.User) // Optional: Include User navigation property if needed
                .Where(e => e.UserId == userId)
                .ToListAsync();
        }

        public async Task AddEnrollmentAsync(int userId, int courseId)
        {
            if (await IsEnrolledAsync(userId, courseId)) return;

            var enrollment = new Enrollments
            {
                UserId = userId,
                CourseId = courseId
            };

            await _context.Enrollments.AddAsync(enrollment);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> IsEnrolledAsync(int userId, int courseId)
        {
            return await _context.Enrollments.AnyAsync(e => e.UserId == userId && e.CourseId == courseId);
        }
        public async Task<Enrollments?> GetEnrollmentAsync(int userId, int courseId)
        {
            return await _dbSet
                .FirstOrDefaultAsync(e => e.UserId == userId && e.CourseId == courseId);
        }

    }
}
