using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Dtos
{
    public class CreateCategoryDto
    {
        public string Name { get; set; } = string.Empty;
        public string? IconName { get; set; }
    }
}
