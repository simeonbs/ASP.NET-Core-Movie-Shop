using MovieShopSystem.Models.Movies;

namespace MovieShopSystem.Models.Api.Movies
{
    public class AllApiRequestModel
    {
        public string Brand { get; init; }

        public string SearchTerm { get; init; }

        public int CurrentPage { get; init; } = 1;

        public int MoviesPerPage { get; init; } = 10;

        public int TotalMovies { get; init; }

        public AllMoviesSorting Sorting { get; init; }
    }
}
