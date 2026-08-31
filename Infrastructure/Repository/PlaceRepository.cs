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
            var place = await _context.Places.FindAsync(id);
            if (place != null)
            {
                _context.Places.Remove(place);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Place>> GetNearbyAsync(double latitude, double longitude, double radiusKm, int? categoryId = null)
        {
            var query = _context.Places
                .Include(p => p.Category)
                .Include(p => p.Images)
                .AsQueryable();

            if (categoryId.HasValue)
            {
                query = query.Where(p => p.CategoryId == categoryId.Value);
            }

            var allPlaces = await query.ToListAsync();

            var nearbyPlaces = allPlaces
                .Select(p => new
                {
                    Place = p,
                    Distance = CalculateHaversineDistance(latitude, longitude, p.Latitude, p.Longitude)
                })
                .Where(x => x.Distance <= radiusKm)
                .OrderBy(x => x.Distance)
                .Select(x => x.Place)
                .ToList();

            return nearbyPlaces;
        }


        public async Task<Place?> GetPlaceById(int id)
        {
            return await _context.Places
                .Include(p => p.Category)
                .Include(p => p.Images)
                .Include(p => p.Reviews)
                .FirstOrDefaultAsync(p => p.Id ==id);
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
            _context.Places.Update(place);
            await _context.SaveChangesAsync();
        }
    }
}
