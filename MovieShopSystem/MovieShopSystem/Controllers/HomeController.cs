using Microsoft.AspNetCore.Mvc;
using MovieShopSystem.Data;
using MovieShopSystem.Models;
using MovieShopSystem.Models.Movies;
using System.Diagnostics;
using System.Linq;

namespace MovieShopSystem.Controllers
{
    public class HomeController : Controller
    {
        private MoviesDbContext data;

        public HomeController(MoviesDbContext data)
        {
            this.data = data;
        }

        public IActionResult Index()
        {
            var movies = this.data
                .Movies
                .OrderByDescending(m => m.Id)
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
                .Take(3)
                .ToList();

            return View(movies);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
