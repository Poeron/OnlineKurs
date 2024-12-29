using OnlineKurs.Models;

namespace OnlineKurs.Repositories.Interfaces
{
    public interface IReviewRepository
    {
        Task<IEnumerable<Reviews>> GetAllAsync();
        Task<Reviews?> GetByIdAsync(int id);
        Task AddAsync(Reviews review);
        Task UpdateAsync(Reviews review);
        Task DeleteAsync(int id);
        Task<IEnumerable<Reviews>> GetReviewsByCourseIdAsync(int courseId);
        Task<IEnumerable<Reviews>> GetReviewsByUserIdAsync(int userId);
    }
}
