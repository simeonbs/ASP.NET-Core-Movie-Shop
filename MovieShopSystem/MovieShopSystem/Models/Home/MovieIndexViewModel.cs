using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopSystem.Models.Home
{
    public class MovieIndexViewModel
    {
        public int Id { get; init; }

        public string Title { get; init; }

        public int ReleasedYear { get; init; }

        public string Writer { get; init; }

        public string ImageUrl { get; init; }

        
    }
}
