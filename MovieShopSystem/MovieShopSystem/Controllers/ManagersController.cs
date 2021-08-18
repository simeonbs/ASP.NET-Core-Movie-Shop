using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieShopSystem.Data.Models;
using MovieShopSystem.Infrastructure;
using MovieShopSystem.Models.Managers;
using MovieShopSystem.Services.Managers;

namespace MovieShopSystem.Controllers
{
    public class ManagersController : Controller
    {
        private readonly IManagerService managers;

        public ManagersController(IManagerService managers)
        {
            this.managers = managers;
        }


        [Authorize]
        public IActionResult Create() => View();

        [HttpPost]
        [Authorize]
        public IActionResult Create(BecomeManagerFormModel manager)
        {
            var id = this.User.GetId();
            var isManager = this.managers.IsManager(id);

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

            this.managers.AddManager(managerData);

            return RedirectToAction("All", "Movies");
        }
    }
}
