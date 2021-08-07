using MovieShopSystem.Models.Movies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopSystem.Services.Movies
{
    public interface IMovieService
    {
        MovieQueryServiceModel All(
            string searchterm,
            AllMoviesSorting sorting,
            int currentPage,
            int moviesPerPage);

        bool Edit(
            int id,
            string title,
            int yearReleased,
            string description,
            string director,
            string writer,
            string imageUrl,
            int genreId);
        MoviesDetailsServiceModel Details(int id);

        IEnumerable<MovieServiceModel> UsersMovies(string userId);

        IEnumerable<string> AllMovieTitles();

        IEnumerable<MovieGenreServiceModel> AllMovieGenres();
    }
}
