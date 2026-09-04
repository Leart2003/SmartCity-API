using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Dtos
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? IconName { get; set; }
    }
}
