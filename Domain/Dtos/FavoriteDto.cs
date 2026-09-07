using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Dtos
{
    public class FavoriteDto
    {
        public int Id { get; set; }
        public int PlaceId { get; set; }
        public string PlaceName { get; set; } = string.Empty;
        public string? CoverImageUrl { get; set; }
        public DateTime AddedAt { get; set; }
    }
}
