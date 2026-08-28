using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Favorite
    {
        public int Id { get; set; }

        public string UserId { get; set; } = string.Empty;
        public AppUser User { get; set; } = null!;

        public int PlaceId { get; set; }
        public Place Place { get; set; } = null!;

        public DateTime AddedAt { get; set; } = DateTime.UtcNow;
    }
}
