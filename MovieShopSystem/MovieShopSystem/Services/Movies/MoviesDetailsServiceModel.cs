namespace MovieShopSystem.Services.Movies
{
    public class MoviesDetailsServiceModel : MovieServiceModel
    {
        public int ManagerId { get; init; }

        public string ManagerName { get; init; }

        public int GenreId { get; init; }

        public string UserId { get; init; }

    }
}
