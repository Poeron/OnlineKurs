using OnlineKurs.Models;
using OnlineKurs.Repositories.Interfaces;

namespace OnlineKurs.Services
{
    public class ReviewService
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<IEnumerable<Reviews>> GetAllReviewsAsync()
        {
            return await _reviewRepository.GetAllAsync();
        }

        public async Task<Reviews?> GetReviewByIdAsync(int id)
        {
            return await _reviewRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Reviews>> GetReviewsByCourseIdAsync(int courseId)
        {
            return await _reviewRepository.GetReviewsByCourseIdAsync(courseId);
        }

        public async Task<IEnumerable<Reviews>> GetReviewsByUserIdAsync(int userId)
        {
            return await _reviewRepository.GetReviewsByUserIdAsync(userId);
        }

        public async Task AddReviewAsync(Reviews review)
        {
            await _reviewRepository.AddAsync(review);
        }

        public async Task UpdateReviewAsync(Reviews review)
        {
            await _reviewRepository.UpdateAsync(review);
        }

        public async Task DeleteReviewAsync(int id)
        {
            await _reviewRepository.DeleteAsync(id);
        }
    }
}
