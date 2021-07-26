using Microsoft.AspNetCore.Mvc;
using MovieShopSystem.Data;
using MovieShopSystem.Data.Models;
using MovieShopSystem.Models.Movies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopSystem.Controllers
{
    public class MoviesController : Controller
    {
        private MoviesDbContext data;

        public MoviesController(MoviesDbContext data)
        {
            this.data = data;
        }


        public IActionResult All([FromQuery]AllMoviesViewModel query)
        {

            
            var moviesQuery = this.data.Movies.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                moviesQuery = moviesQuery.Where(c => c.Title.Contains(query.SearchTerm.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                moviesQuery = moviesQuery.Where(m =>
                    m.Title.ToLower().Contains(query.SearchTerm.ToLower()) ||
                    m.Description.ToLower().Contains(query.SearchTerm.ToLower()));
            }

            moviesQuery = query.Sorting switch
            {
                AllMoviesSorting.Year => moviesQuery.OrderByDescending(m => m.YearReleased),
                AllMoviesSorting.Title => moviesQuery.OrderBy(m => m.Title),
                AllMoviesSorting.DateCreated or _ => moviesQuery.OrderByDescending(m => m.Id)
            };

            var totalMovies = this.data.Movies.Count();

            var cars = moviesQuery
               .Skip((query.CurrentPage - 1) * AllMoviesViewModel.MoviesPerPage)
               .Take(AllMoviesViewModel.MoviesPerPage)
               .Select(m => new MovieListingViewModel
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

            var carBrands = this.data
                .Movies
                .Select(c => c.Title)
                .Distinct()
                .OrderBy(br => br)
                .ToList();

            
            query.Movies = cars;
            query.TotalMovies = totalMovies;

            return View(query);
        }

        public IActionResult Add() => View(new AddMovieFormModel
        {
            Genres = this.GetMovieGenres()
        });

        [HttpPost]
        public IActionResult Add(AddMovieFormModel movie)
        {
            if (!this.data.Genres.Any(c => c.Id == movie.GenreId))
            {
                this.ModelState.AddModelError(nameof(movie.GenreId), "Genre does not exist.");
            }

            if (!ModelState.IsValid)
            {
                movie.Genres = this.GetMovieGenres();

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
                GenreId = movie.GenreId
            };

            this.data.Movies.Add(movieData);
            this.data.SaveChanges();

            return RedirectToAction("All", "Movies");
        }

        private IEnumerable<MovieGenreViewModel> GetMovieGenres()
            => this.data
            .Genres
            .Select(g => new MovieGenreViewModel
            {
                Id = g.Id,
                Name = g.Name
            })
            .ToList();

    }
}
