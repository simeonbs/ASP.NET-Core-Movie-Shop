using MovieShopSystem.Services.Movies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopSystem.Infrastructure
{
    public static class MovieExtensions
    {
        public static string GetInformation(this IMovieModel movie)
            => movie.Title + "-" + movie.YearReleased + "-" + movie.Genre;
    }
}
