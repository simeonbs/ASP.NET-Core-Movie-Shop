using System;

namespace MovieShopSystem.Services.Cart.Models
{
    public class CartServiceModel
    {
        public string CartItemId { get; init; }

        public string UserId { get; init; }

        public DateTime DateCreated { get; init; }

        public int MovieId { get; init; }
    }
}
