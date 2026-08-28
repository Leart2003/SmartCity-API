using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }

        public int Name { get; set; }

        public string? IconName { get; set; }

        public ICollection<Place> Places { get; set; } = new List<Place>();
    }
}
