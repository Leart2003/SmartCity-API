using Domain.Entities;
using Domain.Interface;
using Infrastructure.DB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repository
{
    public class ReviewRepository : IReviewRepository


    {
        private readonly ApplicationDbContext _context;
        public ReviewRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Review> AddAsync(Review review)
        {
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
            return review;
        }

        public async Task DeleteAsync(int id)
        { 
            var review = await _context.Reviews.FindAsync(id);
            if (review is not null)
            {
                _context.Reviews.Remove(review);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<double> GetAverageRatingAsync(int placeId)
        {
            var reviews = await _context.Reviews
                 .Where(r => r.PlaceId == placeId)
                 .ToListAsync();
            if (!reviews.Any())
            {
                return 0;
            }
            return reviews.Average(r => r.Rating);
        }

        public async Task<Review?> GetByIdAsync(int id)
        {
            return await _context.Reviews
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<Review>> GetByPlaceIdAsync(int placeId)
        {
            return await _context.Reviews
                 .Include(r => r.User)
                 .Where(r => r.PlaceId == placeId)
                 .OrderByDescending(r => r.CreatedAt)
                 .ToListAsync();
        }

        public async Task UpdateAsync(Review review)
        {
            _context.Reviews.Update(review);
            await _context.SaveChangesAsync();
        }
    }
}
