using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MovieShopSystem.Models;
using MovieShopSystem.Models.Home;
using MovieShopSystem.Services.Movies;
using MovieShopSystem.Services.Stats;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MovieShopSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStatsService stats;
        private readonly IMemoryCache cache;
        private readonly IMovieService movies;

        public HomeController(
            IStatsService stats,
            IMemoryCache cache,
            IMovieService movies)
        {
            this.stats = stats;
            this.cache = cache;
            this.movies = movies;
        }

        public IActionResult Index()
        {
            const string latestMoviesKey = "LatestMoviesKey";

            var latestMovies = this.cache.Get<List<MovieIndexViewModel>>(latestMoviesKey);

            if (latestMovies == null)
            {
                latestMovies = this.movies.LatestMovies();

                var cacheOpt = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(15));

                this.cache.Set(latestMoviesKey, latestMovies, cacheOpt);
            }

            var stats = this.stats.Total();

            return View(new IndexViewModel
            {
                TotalMovies = stats.TotalMovies,
                TotalUsers = stats.TotalUsers,
                Movies = latestMovies
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
