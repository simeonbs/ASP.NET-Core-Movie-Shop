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
                   GenreName = m.Genre.Name
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

        public IEnumerable<MovieGenreServiceModel> AllMovieGenres()
        => this.data
            .Genres
            .Select(g => new MovieGenreServiceModel
            {
                Id = g.Id,
                Name = g.Name
            })
            .ToList();

        public IEnumerable<string> AllMovieTitles() 
            => this.data
                .Movies
                .Select(c => c.Title)
                .Distinct()
                .OrderBy(br => br)
                .ToList();

        public MoviesDetailsServiceModel Details(int id)
        => this.data.Movies.Where(m => m.Id == id)
            .Select(m => new MoviesDetailsServiceModel
            {
                Id = m.Id,
                Title = m.Title,
                Description = m.Description,
                Director = m.Director,
                GenreName = m.Genre.Name,
                ImageUrl = m.ImageUrl,
                Writer = m.Writer,
                YearReleased = m.YearReleased,
                ManagerId = m.ManagerId,
                ManagerName = m.Manager.Name,
                UserId = m.Manager.UserId
            })
            .FirstOrDefault();

        public bool Edit(int id, string title, int yearReleased, string description, string director, string writer, string imageUrl, int genreId)
        {
            var movieData = this.data.Movies.Find(id);
            if (movieData == null)
            {
                return false;
            }

            movieData.Title = title;
            movieData.YearReleased = yearReleased;
            movieData.Description = description;
            movieData.Director = director;
            movieData.Writer = writer;
            movieData.ImageUrl = imageUrl;
            movieData.GenreId = genreId;

            this.data.SaveChanges();

            return true;
        }

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
                    GenreName = m.Genre.Name
                })
                .ToList();
        }
    }
}
