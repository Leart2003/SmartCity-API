using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Review
    {
        public int Id { get; set; }

        public int PlaceId { get; set; }
        public Place Place { get; set; } = null!;

        public string UserId { get; set; } = string.Empty;
        public AppUser User { get; set; } = null!;

        public int Rating { get; set; } // 1-5
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
