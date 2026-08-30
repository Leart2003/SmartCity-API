using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interface
{
    public interface IFavoriteRepository
    {
        Task<IEnumerable<Favorite>> GetByUserIdAsync(string userId);
        Task<bool> ExistsAsync(string userId, int placeId);
        Task<Favorite> AddAsync(Favorite favorite);
        Task DeleteAsync(int userId, int placeId);
    }
}
