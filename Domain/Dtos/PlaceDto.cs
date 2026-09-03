using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Dtos
{
    
        
    
        public class PlaceDto
        {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Address { get; set; } = string.Empty;
        public string? City { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public double? AverageRating { get; set; }
        public string? CoverImageUrl { get; set; }
        public double? DistanceKm { get; set; }

    }

}

