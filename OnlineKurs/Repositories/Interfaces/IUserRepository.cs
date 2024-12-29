using OnlineKurs.Models;

namespace OnlineKurs.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<Users>> GetAllAsync();
        Task<Users?> GetByIdAsync(int id);
        Task AddAsync(Users user);
        Task UpdateAsync(Users user);
        Task DeleteAsync(int id);
        Task<Users?> GetUserByEmailAsync(string email);
    }
}
