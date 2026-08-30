using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interface
{
    public interface IPlaceImageRepository
    {
        Task<IEnumerable<PlaceImage>> GetByPlaceIdAsync(int placeId);
        Task<PlaceImage?> GetByIdAsync(int id);
        Task<PlaceImage> AddAsync(PlaceImage image);
        Task DeleteAsync(int id);
    }
}
