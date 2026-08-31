using Domain.Entities;
using Domain.Interface;
using Infrastructure.DB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repository
{
    public class PlaceRepository : IPlaceRepository
    {
        private readonly ApplicationDbContext _context;

        public PlaceRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public Task AddAsync(Place place)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Place>> GetNearbyAsync(double latitude, double longitude, double radiusKm, int? categoryId = null)
        {
            throw new NotImplementedException();
        }

        public Task<Place?> GetPlaceById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Place>> GetPlacesAsync()
        {
            return await _context.Places
                .Include(p => p.Category)
                .Include(p => p.Images)
                .ToListAsync();
        }

        public Task UpdateAsync(Place place)
        {
            throw new NotImplementedException();
        }
    }
}
