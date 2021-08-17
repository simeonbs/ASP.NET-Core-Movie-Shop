using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
