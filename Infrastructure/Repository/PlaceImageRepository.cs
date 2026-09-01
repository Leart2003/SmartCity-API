using Domain.Entities;
using Domain.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repository
{
    public class PlaceImageRepository : IPlaceImageRepository
    {
        public Task<PlaceImage> AddAsync(PlaceImage image)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PlaceImage?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PlaceImage>> GetByPlaceIdAsync(int placeId)
        {
            throw new NotImplementedException();
        }
    }
}
