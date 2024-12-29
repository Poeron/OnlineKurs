using OnlineKurs.Models;
using OnlineKurs.Repositories.Interfaces;

namespace OnlineKurs.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<Users>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<Users?> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task AddUserAsync(Users user)
        {
            // İş mantığı: Kullanıcı email doğrulama
            if (await _userRepository.GetUserByEmailAsync(user.Email) != null)
            {
                throw new Exception("Email already exists");
            }
            await _userRepository.AddAsync(user);
        }

        public async Task UpdateUserAsync(Users user)
        {
            await _userRepository.UpdateAsync(user);
        }

        public async Task DeleteUserAsync(int id)
        {
            await _userRepository.DeleteAsync(id);
        }
    }
}
