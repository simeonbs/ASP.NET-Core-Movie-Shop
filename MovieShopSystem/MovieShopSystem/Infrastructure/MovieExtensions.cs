using MovieShopSystem.Services.Movies;

namespace MovieShopSystem.Infrastructure
{
    public static class MovieExtensions
    {
        public static string GetInformation(this IMovieModel movie)
            => movie.Title + "-" + movie.YearReleased + "-" + movie.Genre;
    }
}
