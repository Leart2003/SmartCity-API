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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 

            modelBuilder.Entity<Place>(entity =>
            {
                entity.Property(p => p.Name).IsRequired().HasMaxLength(150);
                entity.Property(p => p.Description).HasMaxLength(2000);
                entity.Property(p => p.Address).HasMaxLength(250);

                entity.HasOne(p => p.Category)
                      .WithMany(c => c.Places)
                      .HasForeignKey(p => p.CategoryId)
                      .OnDelete(DeleteBehavior.Restrict); 
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(c => c.Name).IsRequired().HasMaxLength(100);
            });

         
            modelBuilder.Entity<PlaceImage>(entity =>
            {
                entity.Property(pi => pi.ImageUrl).IsRequired();

                entity.HasOne(pi => pi.Place)
                      .WithMany(p => p.Images)
                      .HasForeignKey(pi => pi.PlaceId)
                      .OnDelete(DeleteBehavior.Cascade); 
            });

       
            modelBuilder.Entity<Review>(entity =>
            {
                entity.Property(r => r.Comment).HasMaxLength(1000);

                entity.HasOne(r => r.Place)
                      .WithMany(p => p.Reviews)
                      .HasForeignKey(r => r.PlaceId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(r => r.User)
                      .WithMany(u => u.Reviews)
                      .HasForeignKey(r => r.UserId)
                      .OnDelete(DeleteBehavior.Restrict); 
            });
           
            modelBuilder.Entity<Favorite>(entity =>
            {
                entity.HasOne(f => f.User)
                      .WithMany(u => u.Favorites)
                      .HasForeignKey(f => f.UserId)
                      .OnDelete(DeleteBehavior.Cascade); 

                entity.HasOne(f => f.Place)
                      .WithMany(p => p.Favorites)
                      .HasForeignKey(f => f.PlaceId)
                      .OnDelete(DeleteBehavior.Cascade); 
             
                entity.HasIndex(f => new { f.UserId, f.PlaceId }).IsUnique();
            });


        }

    }
