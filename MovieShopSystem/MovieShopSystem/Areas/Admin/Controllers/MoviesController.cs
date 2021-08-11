using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopSystem.Areas.Admin.Controllers
{
    public class MoviesController : Controller
    {
        public IActionResult Index() => View();
    }
}
