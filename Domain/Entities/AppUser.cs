using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
namespace Domain.Entities
{
    public class AppUser : IdentityUser
    {
        public string FullName{ get; set; }
        public string Role { get; set; } = "Tourist";

        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
