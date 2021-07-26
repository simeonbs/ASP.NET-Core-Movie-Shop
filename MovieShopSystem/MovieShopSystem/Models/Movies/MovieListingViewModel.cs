using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopSystem.Models.Movies
{
    public class MovieListingViewModel
    {
        public int Id { get; init; }

        public string Title { get; set; }

        public int YearReleased { get; set; }

        public string Description { get; set; }

        public string Director { get; set; }

        public string Writer { get; set; }

        public string ImageUrl { get; set; }

        public string Genre { get; init; }
    }
}
