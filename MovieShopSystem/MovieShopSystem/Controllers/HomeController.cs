using Microsoft.AspNetCore.Mvc;
using MovieShopSystem.Data;
using MovieShopSystem.Models;
using MovieShopSystem.Models.Home;
using MovieShopSystem.Models.Movies;
using MovieShopSystem.Services.Stats;
using System.Diagnostics;
using System.Linq;

namespace MovieShopSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly MoviesDbContext data;
        private readonly IStatsService stats;

        public HomeController(
            IStatsService stats,
            MoviesDbContext data)
        {
            this.stats = stats;
            this.data = data;
        }

        public IActionResult Index()
        {
            var movies = this.data
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

            var stats = this.stats.Total();

            return View(new IndexViewModel
            {
                TotalMovies = stats.TotalMovies,
                TotalUsers = stats.TotalUsers,
                Movies = movies
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
