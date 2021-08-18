using MovieShopSystem.Data;
using MovieShopSystem.Data.Models;
using MovieShopSystem.Services.Cart.Models;
using System.Collections.Generic;
using System.Linq;

namespace MovieShopSystem.Services.Cart
{
    public class CartService : ICartService
    {
        private readonly MoviesDbContext data;

        public CartService(MoviesDbContext data)
        {
            this.data = data;
        }

        public IEnumerable<CartItemViewServiceModel> UsersCart(string userId)
        {
            var managerId = this.data.Movies.Select(p => p.ManagerId).FirstOrDefault();
            var manager = this.data.Managers.FirstOrDefault(s => s.Id == managerId);

            return data.CartItems
            .Where(sci => sci.UserId == userId)
            .Select(sci => new CartItemViewServiceModel
            {
                Title = sci.Movie.Title,
                ImageURL = sci.Movie.ImageUrl,
                Price = sci.Movie.Price,
                Director = sci.Movie.Director,
                Manager = manager,
                ManagerId = managerId,
                MovieId = sci.MovieId,
            })
            .ToList();
        }

        public bool AddProductToCart(int movieId, string userId)
        {
            var user = data.Users
                .Where(u => u.Id == userId)
                .FirstOrDefault();

            if (user == null)
            {
                return false;
            }

            var product = data.Movies
                .Where(p => p.Id == movieId)
                .FirstOrDefault();

            if (product == null)
            {
                return false;
            }

            if (this.data.CartItems.Where(x => x.UserId == userId).Any(x => x.MovieId == movieId))
            {
                return false;
            }

            var cartItem = new Item()
            {
                UserId = userId,
                MovieId = movieId
            };

            data.CartItems.Add(cartItem);
            data.SaveChanges();

            return true;
        }

        public bool Delete(int movieId, string userId)
        {
            var product = this.data.CartItems.Where(p => p.MovieId == movieId && p.UserId == userId).FirstOrDefault();
            if (product == null)
            {
                return false;
            }

            this.data.CartItems.Remove(product);
            this.data.SaveChanges();
            return true;
        }

        public bool DeleteAll(string userId)
        {
            foreach (var movie in this.data.CartItems.Where(u => u.UserId == userId).ToList())
            {
                var movieId = this.data.Movies.Where(m => m.Id == movie.MovieId).FirstOrDefault();
                if (movieId == null)
                {
                    return false;
                }
                this.data.CartItems.Remove(movie);
                this.data.Movies.Remove(movieId);
                this.data.SaveChanges();
            }

            return true;
        }
    }
}
