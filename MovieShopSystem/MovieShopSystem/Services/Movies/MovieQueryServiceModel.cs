using System.Collections.Generic;

namespace MovieShopSystem.Services.Movies
{
    public class MovieQueryServiceModel
    {
        public int CurrentPage { get; init; }

        public int TotalMovies { get; set; }

        public int MoviesPerPage { get; init; }

        public IEnumerable<MovieServiceModel> Movies { get; init; }
    }
}
