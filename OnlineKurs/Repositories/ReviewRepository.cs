﻿using OnlineKurs.Models;
using OnlineKurs.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using OnlineKurs.Data;

namespace OnlineKurs.Repositories
{
    public class ReviewRepository : BaseRepository<Reviews>, IReviewRepository
    {
        public ReviewRepository(OnlineKursContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Reviews>> GetReviewsByCourseIdAsync(int courseId)
        {
            return await _dbSet.Where(r => r.CourseId == courseId).ToListAsync();
        }

        public async Task<IEnumerable<Reviews>> GetReviewsByUserIdAsync(int userId)
        {
            return await _dbSet.Where(r => r.UserId == userId).ToListAsync();
        }
    }
}
