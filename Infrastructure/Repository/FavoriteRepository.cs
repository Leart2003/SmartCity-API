using Domain.Entities;
using Domain.Interface;
using Infrastructure.DB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repository
{
    public class FavoriteRepository : IFavoriteRepository
    {
        private readonly ApplicationDbContext _context;
        public FavoriteRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Favorite> AddAsync(Favorite favorite)
        {
            _context.AddAsync(favorite);
            await _context.SaveChangesAsync();
            return favorite;
        }

        public async Task DeleteAsync(string userId, int placeId)
        {
            var favorite = await _context.Favorites
                .FirstOrDefaultAsync(f => f.UserId == userId && f.PlaceId == placeId);

            if (favorite != null)
            {
                _context.Favorites.Remove(favorite);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(string userId, int placeId)
        {
            return await _context.Favorites
                .AnyAsync(f => f.UserId == userId && f.PlaceId == placeId);
        }

        public async Task<IEnumerable<Favorite>> GetByUserIdAsync(string userId)
        {
         return await _context.Favorites
                .Include(f => f.Place)
                .ThenInclude(p => p.Category)
                .Where(f => f.UserId == userId)
                .ToListAsync();
        }
    }
}
