using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interface
{
    public interface IReviewRepository
    {

        Task<IEnumerable<Review>> GetByPlaceIdAsync(int placeId);
        Task<Review?> GetByIdAsync(int id);
        Task<double> GetAverageRatingAsync(int placeId);
        Task<Review> AddAsync(Review review);
        Task UpdateAsync(Review review);
        Task DeleteAsync(int id);
    }
}
