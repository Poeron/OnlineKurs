using OnlineKurs.Shared.Models;
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
        public async Task<Courses?> GetByIdAsync(int id)
        {
            return await _dbSet.Include(c => c.User) // Include User navigation property
                               .FirstOrDefaultAsync(c => c.Id == id);
        }

    }
}
