using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Place
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }


        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public string Address { get; set; } = string.Empty;
        public string? City { get; set; }

        //foreinKeys

        public int CategoryId { get; set; }

        public Category Category { get; set; }


        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<PlaceImage> Images { get; set; } = new List<PlaceImage>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();


    }
}
