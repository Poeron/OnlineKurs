using OnlineKurs.Shared.Models;

namespace OnlineKurs.Repositories.Interfaces
{
    public interface IEnrollmentRepository
    {
        Task<IEnumerable<Enrollments>> GetAllAsync();
        Task<Enrollments?> GetByIdAsync(int id);
        Task AddAsync(Enrollments enrollment);
        Task UpdateAsync(Enrollments enrollment);
        Task DeleteAsync(int id);
        Task<IEnumerable<Enrollments>> GetEnrollmentsByCourseIdAsync(int courseId);
        Task<IEnumerable<Enrollments>> GetEnrollmentsByUserIdAsync(int userId);
        Task<bool> IsEnrolledAsync(int userId, int courseId);
        Task AddEnrollmentAsync(int userId, int courseId);
        Task<Enrollments?> GetEnrollmentAsync(int userId, int courseId);

    }
}
