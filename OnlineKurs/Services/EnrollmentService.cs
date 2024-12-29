using OnlineKurs.Models;
using OnlineKurs.Repositories.Interfaces;

namespace OnlineKurs.Services
{
    public class EnrollmentService
    {
        private readonly IEnrollmentRepository _enrollmentRepository;

        public EnrollmentService(IEnrollmentRepository enrollmentRepository)
        {
            _enrollmentRepository = enrollmentRepository;
        }

        public async Task<IEnumerable<Enrollments>> GetAllEnrollmentsAsync()
        {
            return await _enrollmentRepository.GetAllAsync();
        }

        public async Task<Enrollments?> GetEnrollmentByIdAsync(int id)
        {
            return await _enrollmentRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Enrollments>> GetEnrollmentsByCourseIdAsync(int courseId)
        {
            return await _enrollmentRepository.GetEnrollmentsByCourseIdAsync(courseId);
        }

        public async Task<IEnumerable<Enrollments>> GetEnrollmentsByUserIdAsync(int userId)
        {
            return await _enrollmentRepository.GetEnrollmentsByUserIdAsync(userId);
        }

        public async Task AddEnrollmentAsync(Enrollments enrollment)
        {
            await _enrollmentRepository.AddAsync(enrollment);
        }

        public async Task UpdateEnrollmentAsync(Enrollments enrollment)
        {
            await _enrollmentRepository.UpdateAsync(enrollment);
        }

        public async Task DeleteEnrollmentAsync(int id)
        {
            await _enrollmentRepository.DeleteAsync(id);
        }
    }
}
