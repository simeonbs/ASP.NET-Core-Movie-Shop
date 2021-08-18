using MovieShopSystem.Data.Models;
using MovieShopSystem.Models.Home;
using MovieShopSystem.Models.Movies;
using System.Collections.Generic;
    
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

        List<MovieIndexViewModel> LatestMovies();

        int GetManagerId(string id);

        bool AddMovie(Movie movie);

        bool AnyMovieGenre(MovieFormModel movie);

        Movie GetMovie(int id);

        bool RemoveMovie(Movie movie);

        IEnumerable<MovieGenreServiceModel> AllMovieGenres();
    }
}
