using Microsoft.AspNetCore.Mvc;

namespace MovieShopSystem.Areas.Admin.Controllers
{
    public class MoviesController : Controller
    {
        public IActionResult Index() => View();
    }
}
