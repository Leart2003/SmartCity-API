using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class PlaceImage
    {

        public int Id { get; set; }

        public int PlaceId { get; set; }
        public Place Place { get; set; } = null!;

        public string ImageUrl { get; set; } = string.Empty;
        public bool IsCoverImage { get; set; }
    }
}
