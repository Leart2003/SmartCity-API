using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Dtos
{
    public class CreatePlaceImageDto
    {
        public int PlaceId { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public bool IsCoverImage { get; set; }
    }
}
