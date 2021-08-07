using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieShopSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

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

            base.OnModelCreating(builder);
        }

    }
}
