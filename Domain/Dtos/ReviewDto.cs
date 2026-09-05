using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Dtos
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public int PlaceId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string UserFullName { get; set; } = string.Empty;
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
