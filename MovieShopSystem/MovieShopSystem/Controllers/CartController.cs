using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieShopSystem.Infrastructure;
using MovieShopSystem.Services.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopSystem.Controllers
{
    using static WebConstants;
    public class CartController : Controller
    {
        private readonly ICartService cartService;

        public CartController(ICartService cartService)
        {
            this.cartService = cartService;
        }

        [Authorize]
        public IActionResult MyCart(string userId)
        {
            if (this.User.GetId() != userId || User.IsAdmin())
            {
                return Unauthorized();
            }

            var usersProducts = cartService.UsersCart(userId);

            return View(usersProducts);
        }

        [Authorize]
        public IActionResult AddToCart(int movieId, string userId)
        {

            if (this.User.GetId() != userId || User.IsAdmin())
            {
                return Unauthorized();
            }

            cartService.AddProductToCart(movieId, userId);

            TempData[GlobalMessageKey] = "The movie has been added to your cart!";

            return RedirectToAction("All", "Movies");
        }

        [Authorize]
        public IActionResult Delete(int movieId, string userId)
        {
            cartService.Delete(movieId, userId);

            return RedirectToAction($"MyCart", new { userId = this.User.GetId() });
        }

        [Authorize]
        public IActionResult Purachase()
        {
            cartService.DeleteAll(this.User.GetId());

            TempData[GlobalMessageKey] = "Purchase completed successfully!";

            return RedirectToAction("All", "Movies");
        }
    }
}
