using OnlineKurs.Models;
using OnlineKurs.Repositories.Interfaces;

namespace OnlineKurs.Services
{
    public class CourseService
    {
        private readonly ICourseRepository _courseRepository;

        public CourseService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<IEnumerable<Courses>> GetAllCoursesAsync()
        {
            return await _courseRepository.GetAllAsync();
        }

        public async Task<Courses?> GetCourseByIdAsync(int id)
        {
            return await _courseRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Courses>> GetCoursesByUserIdAsync(int userId)
        {
            return await _courseRepository.GetCoursesByUserIdAsync(userId);
        }

        public async Task AddCourseAsync(Courses course)
        {
            await _courseRepository.AddAsync(course);
        }

        public async Task UpdateCourseAsync(Courses course)
        {
            await _courseRepository.UpdateAsync(course);
        }

        public async Task DeleteCourseAsync(int id)
        {
            await _courseRepository.DeleteAsync(id);
        }
    }
}
