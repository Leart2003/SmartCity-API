using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interface
{
    public interface IPlaceRepository
    {
        Task<IEnumerable<Place>> GetPlacesAsync();

        Task<Place?> GetPlaceById(int id);

        Task<IEnumerable<Place>> GetNearbyAsync(double latitude, double longitude, double radiusKm, int? categoryId = null);

        Task AddAsync(Place place);
        Task UpdateAsync(Place place);

        Task DeleteAsync(int id);



    }
}
