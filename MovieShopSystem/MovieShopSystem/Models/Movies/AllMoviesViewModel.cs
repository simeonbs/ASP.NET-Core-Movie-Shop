using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        public IEnumerable<string> Titles { get; set; }

        public IEnumerable<MovieListingViewModel> Movies { get; set; }
    }
}
