using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieShopSystem.Data.Models;

namespace MovieShopSystem.Data
{
    public class MoviesDbContext : IdentityDbContext<User>
    {
        public MoviesDbContext(DbContextOptions<MoviesDbContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movies { get; init; }

        public DbSet<Genre> Genres { get; init; }

        public DbSet<Manager> Managers { get; init; }

        public DbSet<Item> CartItems { get; init; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Movie>()
                .HasOne(c => c.Genre)
                .WithMany(c => c.Movies)
                .HasForeignKey(c => c.GenreId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Movie>()
                .HasOne(m => m.Manager)
                .WithMany(m => m.Movies)
                .HasForeignKey(m => m.ManagerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Manager>()
                .HasOne<User>()
                .WithOne()
                .HasForeignKey<Manager>(m => m.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Item>()
                .HasKey(x => new { x.MovieId, x.UserId });

            base.OnModelCreating(builder);
        }

    }
}
