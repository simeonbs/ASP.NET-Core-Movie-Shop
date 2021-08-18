using Microsoft.AspNetCore.Mvc;
using MovieShopSystem.Data;
using MovieShopSystem.Models.Api.Movies;
using MovieShopSystem.Services.Movies;

namespace MovieShopSystem.Controllers.Api
{
    [ApiController]
    [Route("api/movies")]
    public class MoviesApiController : ControllerBase
    {
        private readonly IMovieService movies;

        public MoviesApiController(IMovieService movies)
            => this.movies = movies;
        

        [HttpGet]
        public ActionResult<MovieQueryServiceModel> All([FromQuery] AllApiRequestModel query) 
            => this.movies.All(query.SearchTerm, query.Sorting, query.CurrentPage, query.MoviesPerPage);
    }
}
