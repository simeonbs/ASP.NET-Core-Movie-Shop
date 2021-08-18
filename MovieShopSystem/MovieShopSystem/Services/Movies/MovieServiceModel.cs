namespace MovieShopSystem.Services.Movies
{
    public class MovieServiceModel
    {
        public int Id { get; init; }

        public decimal Price { get; set; }

        public string Title { get; set; }

        public int YearReleased { get; set; }

        public string Description { get; set; }

        public string Director { get; set; }

        public string Writer { get; set; }

        public string ImageUrl { get; set; }

        public string GenreName { get; init; }
    }
}
