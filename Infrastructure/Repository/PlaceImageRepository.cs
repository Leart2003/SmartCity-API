using Domain.Entities;
using Domain.Interface;
using Infrastructure.DB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repository
{
    public class PlaceImageRepository : IPlaceImageRepository
    {
        private readonly ApplicationDbContext _context;

        public PlaceImageRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<PlaceImage> AddAsync(PlaceImage image)
        {
            _context.PlaceImages.AddAsync(image);
            await _context.SaveChangesAsync();
            return image;
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<PlaceImage?> GetByIdAsync(int id)
        {
            return await _context.PlaceImages.FindAsync(id);

        }

        public async Task<IEnumerable<PlaceImage>> GetByPlaceIdAsync(int placeId)
        {
            var ListImages =
            _context.PlaceImages.Where(pi => pi.PlaceId == placeId).ToListAsync();

            return await ListImages;
        }
    }
}
