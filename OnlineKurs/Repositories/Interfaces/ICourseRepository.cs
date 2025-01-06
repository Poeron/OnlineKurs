using OnlineKurs.Shared.Models;

namespace OnlineKurs.Repositories.Interfaces
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Courses>> GetAllAsync();
        Task<Courses?> GetByIdAsync(int id);
        Task AddAsync(Courses course);
        Task UpdateAsync(Courses course);
        Task DeleteAsync(int id);
        Task<IEnumerable<Courses>> GetCoursesByUserIdAsync(int userId);
    }
}
