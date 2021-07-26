using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopSystem.Models.Movies
{
    public class AllMoviesViewModel
    {
        public const int MoviesPerPage = 2;

        public string Brand { get; init; }

        [Display(Name = "Search by text")]
        public string SearchTerm { get; init; }

        public int CurrentPage { get; init; } = 1;

        public int TotalMovies { get; set; }

        public AllMoviesSorting Sorting { get; init; }

        public IEnumerable<string> Brands { get; set; }

        public IEnumerable<MovieListingViewModel> Movies { get; set; }
    }
}
