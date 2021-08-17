using MovieShopSystem.Services.Cart.Models;
using System.Collections.Generic;

namespace MovieShopSystem.Services.Cart
{
    public interface ICartService
    {
        public IEnumerable<CartItemViewServiceModel> UsersCart(string userId);

        public bool AddProductToCart(int movieId, string userId);

        public bool Delete(int movieId, string user);
        public bool DeleteAll(string userId);
    }
}