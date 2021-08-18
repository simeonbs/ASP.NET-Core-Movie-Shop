namespace MovieShopSystem.Services.Movies
{
    public interface IMovieModel
    {
        string Title { get; }

        string Genre { get; }

        int YearReleased { get; }
    }
}
