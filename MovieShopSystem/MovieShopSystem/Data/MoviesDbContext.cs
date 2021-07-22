using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieShopSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieShopSystem.Data
{
    public class MoviesDbContext : IdentityDbContext
    {
        public MoviesDbContext(DbContextOptions<MoviesDbContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movies { get; init; }

        public DbSet<Genre> Genres { get; init; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Movie>()
                .HasOne(c => c.Genre)
                .WithMany(c => c.Movies)
                .HasForeignKey(c => c.GenreId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }

    }
}
