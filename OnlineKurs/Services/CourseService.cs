using OnlineKurs.Shared.Models;
using OnlineKurs.Repositories.Interfaces;

namespace OnlineKurs.Services;

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
    public async Task<IEnumerable<Courses>> GetRelatedCoursesAsync(int userId, int currentCourseId)
    {
        var courses = await _courseRepository.GetCoursesByUserIdAsync(userId);
        return courses.Where(c => c.Id != currentCourseId); // Exclude the current course
    }

}
