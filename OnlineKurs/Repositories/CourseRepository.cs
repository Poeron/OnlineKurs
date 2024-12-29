using OnlineKurs.Models;
using OnlineKurs.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using OnlineKurs.Data;

namespace OnlineKurs.Repositories
{
    public class CourseRepository : BaseRepository<Courses>, ICourseRepository
    {
        public CourseRepository(OnlineKursContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Courses>> GetCoursesByUserIdAsync(int userId)
        {
            return await _dbSet.Where(c => c.UserId == userId).ToListAsync();
        }
    }
}
