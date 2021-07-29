using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieShopSystem.Data;
using MovieShopSystem.Data.Models;
using MovieShopSystem.Infrastructure;
using MovieShopSystem.Models.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopSystem.Controllers
{
    public class ManagersController : Controller
    {
        private readonly MoviesDbContext data;

        public ManagersController(MoviesDbContext data) => this.data = data;

        [Authorize]
        public IActionResult Create() => View();

        [HttpPost]
        [Authorize]
        public IActionResult Create(BecomeManagerFormModel manager)
        {
            var id = this.User.GetId();
            var isManager = this.data
                .Managers
                .Any(m => m.UserId == id);

            if (isManager)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(manager);
            }

            var managerData = new Manager
            {
                Name = manager.Name,
                PhoneNumber = manager.PhoneNumber,
                UserId = id
            };

            this.data.Managers.Add(managerData);
            this.data.SaveChanges();

            return RedirectToAction("All", "Movies");
        }
    }
}
