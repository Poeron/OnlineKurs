using OnlineKurs.Models;
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
            return await _dbSet.Where(e => e.UserId == userId).ToListAsync();
        }
    }
}
