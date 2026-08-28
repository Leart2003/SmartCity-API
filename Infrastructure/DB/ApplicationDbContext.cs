using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Infrastructure.DB
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        { }
        public DbSet<Place> Places { get; set; } = null!;

        public DbSet<Category> Categories { get; set; }

        public DbSet<PlaceImage> PlaceImages { get; set; } = null!;
        public DbSet<Review> Reviews { get; set; } = null!;

        public DbSet<Favorite> Favorites { get; set; } = null!;



    } 
    
}
