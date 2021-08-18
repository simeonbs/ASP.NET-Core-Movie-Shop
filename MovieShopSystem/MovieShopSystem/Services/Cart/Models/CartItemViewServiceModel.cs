using MovieShopSystem.Data.Models;

namespace MovieShopSystem.Services.Cart.Models
{
    public class CartItemViewServiceModel
    {
        public int MovieId { get; init; }

        public string Title { get; init; }

        public string ImageURL { get; init; }

        public string Director { get; init; }

        public decimal Price { get; init; }

        public int ManagerId { get; init; }

        public Manager Manager { get; init; }
    }
}
