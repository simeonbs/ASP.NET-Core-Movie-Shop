using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieShopSystem.Data;
using MovieShopSystem.Data.Models;
using MovieShopSystem.Infrastructure;
using MovieShopSystem.Models.Movies;
using MovieShopSystem.Services.Managers;
using MovieShopSystem.Services.Movies;
using System.Linq;

namespace MovieShopSystem.Controllers
{
    using static WebConstants;
    public class MoviesController : Controller
    {
        private readonly IMovieService movies;
        private readonly IManagerService managers;

        public MoviesController(
            IMovieService movies,
            IManagerService managers)
        {
            this.movies = movies;
            this.managers = managers;
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
            if (!this.managers.IsManager(this.User.GetId()))
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
            var managerId = this.movies.GetManagerId(this.User.GetId());

            if (managerId == 0)
            {
                return RedirectToAction(nameof(ManagersController.Create), "Managers");
            }

            if (!this.movies.AnyMovieGenre(movie))
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
                Price = movie.Price,
                Description = movie.Description,
                Director = movie.Director,
                Writer = movie.Writer,
                ImageUrl = movie.ImageUrl,
                GenreId = movie.GenreId,
                ManagerId = managerId
            };

            this.movies.AddMovie(movieData);

            TempData[GlobalMessageKey] = "Your product has been added!";

            return RedirectToAction("All", "Movies");
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            if (!this.managers.IsManager(this.User.GetId()) && !User.IsAdmin())
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
            var managerId = this.movies.GetManagerId(this.User.GetId());

            if (managerId == 0 && !User.IsAdmin())
            {
                return RedirectToAction(nameof(ManagersController.Create), "Managers");
            }

            if (!this.movies.AnyMovieGenre(movie))
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

        public IActionResult Details(int id, string information)
        {
            var movie = this.movies.Details(id);


            return View(movie);
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var movie = this.movies.GetMovie(id);

            var manager = this.managers.GetManager(this.User.GetId());

            if (movie.ManagerId != manager.Id && !this.User.IsAdmin())
            {
                return Unauthorized();
            }

            return View(movie);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var movie = this.movies.GetMovie(id);
            this.movies.RemoveMovie(movie);
            TempData[GlobalMessageKey] = "Successfully deleted!";
            return RedirectToAction("MyMovies", "Movies");
        }
    }
}
