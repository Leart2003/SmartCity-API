using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Dtos
{
    public class CreateReviewDto
    {
        public int PlaceId { get; set; }
        public int Rating { get; set; } 
        public string? Comment { get; set; }
    }
}
