using MovieShopSystem.Data;
using MovieShopSystem.Models.Movies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopSystem.Services.Movies
{
    public class MovieService : IMovieService
    {
        private MoviesDbContext data;

        public MovieService(MoviesDbContext data)
        {
            this.data = data;
        }

        public MovieQueryServiceModel All(string searchterm,AllMoviesSorting sorting, int currentPage, int moviesPerPage)
        {
            var moviesQuery = this.data.Movies.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchterm))
            {
                moviesQuery = moviesQuery.Where(c => c.Title.Contains(searchterm.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(searchterm))
            {
                moviesQuery = moviesQuery.Where(m =>
                    m.Title.ToLower().Contains(searchterm.ToLower()) ||
                    m.Description.ToLower().Contains(searchterm.ToLower()));
            }

            moviesQuery = sorting switch
            {
                AllMoviesSorting.Year => moviesQuery.OrderByDescending(m => m.YearReleased),
                AllMoviesSorting.Title => moviesQuery.OrderBy(m => m.Title),
                AllMoviesSorting.DateCreated or _ => moviesQuery.OrderByDescending(m => m.Id)
            };

            var totalMovies = this.data.Movies.Count();

            var movies = moviesQuery
               .Skip((currentPage - 1) * moviesPerPage)
               .Take(moviesPerPage)
               .Select(m => new MovieServiceModel
               {
                   Id = m.Id,
                   Title = m.Title,
                   YearReleased = m.YearReleased,
                   Description = m.Description,
                   Director = m.Director,
                   Writer = m.Writer,
                   ImageUrl = m.ImageUrl,
                   Genre = m.Genre.Name
               })
               .ToList();

            return new MovieQueryServiceModel
            {
                TotalMovies = totalMovies,
                CurrentPage = currentPage,
                MoviesPerPage = moviesPerPage,
                Movies = movies
            };
        }

        public IEnumerable<string> AllMovieTitles() 
            => this.data
                .Movies
                .Select(c => c.Title)
                .Distinct()
                .OrderBy(br => br)
                .ToList();

        public IEnumerable<MovieServiceModel> UsersMovies(string userId)
        {
            return this.data
                .Movies
                .Where(m => m.Manager.UserId == userId)
                .Select(m => new MovieServiceModel
                {
                    Id = m.Id,
                    Title = m.Title,
                    YearReleased = m.YearReleased,
                    Description = m.Description,
                    Director = m.Director,
                    Writer = m.Writer,
                    ImageUrl = m.ImageUrl,
                    Genre = m.Genre.Name
                })
                .ToList();
        }
    }
}
