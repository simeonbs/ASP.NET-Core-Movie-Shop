using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MovieShopSystem.Data;
using MovieShopSystem.Models;
using MovieShopSystem.Models.Home;
using MovieShopSystem.Models.Movies;
using MovieShopSystem.Services.Stats;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MovieShopSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly MoviesDbContext data;
        private readonly IStatsService stats;
        private readonly IMemoryCache cache;

        public HomeController(
            IStatsService stats,
            MoviesDbContext data,
            IMemoryCache cache)
        {
            this.stats = stats;
            this.data = data;
            this.cache = cache;
        }

        public IActionResult Index()
        {
            const string latestMoviesKey = "LatestMoviesKey";

            var latestMovies = this.cache.Get<List<MovieIndexViewModel>>(latestMoviesKey);

            if (latestMovies == null)
            {
                latestMovies = this.data
                .Movies
                .OrderByDescending(m => m.Id)
                .Select(m => new MovieIndexViewModel
                {
                    Id = m.Id,
                    Title = m.Title,
                    ReleasedYear = m.YearReleased,
                    Writer = m.Writer,
                    ImageUrl = m.ImageUrl,
                })
                .Take(3)
                .ToList();

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
