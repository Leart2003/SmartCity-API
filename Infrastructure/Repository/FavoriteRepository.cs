using Domain.Entities;
using Domain.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repository
{
    public class FavoriteRepository : IFavoriteRepository
    {
        public Task<Favorite> AddAsync(Favorite favorite)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(string userId, int placeId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsAsync(string userId, int placeId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Favorite>> GetByUserIdAsync(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
