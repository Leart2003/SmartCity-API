using Domain.Entities;
using Domain.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repository
{
    public class ReviewRepistory : IReviewRepository_

    {
        public Task<Review> AddAsync(Review review)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<double> GetAverageRatingAsync(int placeId)
        {
            throw new NotImplementedException();
        }

        public Task<Review?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Review>> GetByPlaceIdAsync(int placeId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Review review)
        {
            throw new NotImplementedException();
        }
    }
}
