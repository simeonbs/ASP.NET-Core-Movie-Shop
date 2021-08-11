using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieShopSystem.Data;
using MovieShopSystem.Data.Models;
using MovieShopSystem.Infrastructure;
using MovieShopSystem.Models.Movies;
using MovieShopSystem.Services.Movies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MovieShopSystem.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieService movies;
        private MoviesDbContext data;

        public MoviesController(IMovieService movies, MoviesDbContext data)
        {
            this.movies = movies;
            this.data = data;
        }


        public IActionResult All([FromQuery] AllMoviesViewModel query)
        {
            var movies = this.movies.All(
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                AllMoviesViewModel.MoviesPerPage);

            var movieTitles = this.movies.AllMovieTitles();


            query.Titles = movieTitles;
            query.TotalMovies = movies.TotalMovies;
            query.Movies = movies.Movies.Select(m => new MovieListingViewModel
            {
                Id = m.Id,
                Title = m.Title,
                Description = m.Description,
                Director = m.Director,
                Genre = m.GenreName,
                ImageUrl = m.ImageUrl,
                Writer = m.Writer,
                YearReleased = m.YearReleased
            });

            return View(query);
        }

        [Authorize]
        public IActionResult MyMovies()
        {
            var movies = this.movies.UsersMovies(this.User.GetId());

            return View(movies);
        }


        [Authorize]
        public IActionResult Add()
        {
            if (!this.UserIsManager())
            {
                return RedirectToAction(nameof(ManagersController.Create), "Managers");
            }

            return View(new MovieFormModel
            {
                Genres = this.movies.AllMovieGenres()
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(MovieFormModel movie)
        {
            var managerId = this.data
                .Managers
                .Where(m => m.UserId == this.User.GetId())
                .Select(m => m.Id)
                .FirstOrDefault();

            if (managerId == 0)
            {
                return RedirectToAction(nameof(ManagersController.Create), "Managers");
            }

            if (!this.data.Genres.Any(c => c.Id == movie.GenreId))
            {
                this.ModelState.AddModelError(nameof(movie.GenreId), "Genre does not exist.");
            }

            if (!ModelState.IsValid)
            {
                movie.Genres = this.movies.AllMovieGenres();

                return View(movie);
            }

            var movieData = new Movie
            {

                Title = movie.Title,
                YearReleased = movie.YearReleased,
                Description = movie.Description,
                Director = movie.Director,
                Writer = movie.Writer,
                ImageUrl = movie.ImageUrl,
                GenreId = movie.GenreId,
                ManagerId = managerId
            };

            this.data.Movies.Add(movieData);
            this.data.SaveChanges();

            return RedirectToAction("All", "Movies");
        }
        private bool UserIsManager()
            => this.data
                .Managers
                .Any(m => m.UserId == this.User.GetId());


        [Authorize]
        public IActionResult Edit(int id)
        {
            if (!this.UserIsManager() && !User.IsAdmin())
            {
                return RedirectToAction(nameof(ManagersController.Create), "Managers");
            }

            var movie = this.movies.Details(id);

            if (movie.UserId != this.User.GetId() && !User.IsAdmin())
            {
                return BadRequest();
            }

            return View(new MovieFormModel
            {
                Title = movie.Title,
                YearReleased = movie.YearReleased,
                Description = movie.Description,
                Director = movie.Director,
                Writer = movie.Writer,
                ImageUrl = movie.ImageUrl,
                GenreId = movie.GenreId,
                Genres = this.movies.AllMovieGenres()
            });
        }


        [HttpPost]
        [Authorize]
        public IActionResult Edit(int id, MovieFormModel movie)
        {
            var managerId = this.data
                .Managers
                .Where(m => m.UserId == this.User.GetId())
                .Select(m => m.Id)
                .FirstOrDefault();

            if (managerId == 0 && !User.IsAdmin())
            {
                return RedirectToAction(nameof(ManagersController.Create), "Managers");
            }

            if (!this.data.Genres.Any(c => c.Id == movie.GenreId))
            {
                this.ModelState.AddModelError(nameof(movie.GenreId), "Genre does not exist.");
            }

            if (!ModelState.IsValid)
            {
                movie.Genres = this.movies.AllMovieGenres();

                return View(movie);
            }

            var edited = this.movies.Edit(
                id,
                movie.Title,
                movie.YearReleased,
                movie.Description,
                movie.Director,
                movie.Writer,
                movie.ImageUrl,
                movie.GenreId);

            if (!edited)
            {
                return BadRequest();
            }

            this.movies.Edit(
                id,
                movie.Title,
                movie.YearReleased,
                movie.Description,
                movie.Director,
                movie.Writer,
                movie.ImageUrl,
                movie.GenreId);


            return RedirectToAction(nameof(All));
        }

        //public IActionResult Details(int id, string information)
        //{
        //    var movie = this.movies.Details(id);

        //    if (information != movie.GetInformation())
        //    {
        //        return BadRequest();
        //    }

        //    return View(movie);
        //}

    }
}
