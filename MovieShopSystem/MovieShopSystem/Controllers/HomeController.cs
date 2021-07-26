using Microsoft.AspNetCore.Mvc;
using MovieShopSystem.Data;
using MovieShopSystem.Models;
using MovieShopSystem.Models.Home;
using MovieShopSystem.Models.Movies;
using System.Diagnostics;
using System.Linq;

namespace MovieShopSystem.Controllers
{
    public class HomeController : Controller
    {
        private MoviesDbContext data;

        public HomeController(MoviesDbContext data) 
            => this.data = data;

        public IActionResult Index()
        {
            var totalMovies = this.data.Movies.Count();

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

            return View(new IndexViewModel
            {
                TotalMovies = totalMovies,
                Movies = movies
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
