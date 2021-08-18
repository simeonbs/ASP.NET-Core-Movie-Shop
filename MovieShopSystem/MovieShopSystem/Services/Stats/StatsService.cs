using MovieShopSystem.Data;
using System.Linq;

namespace MovieShopSystem.Services.Stats
{
    public class StatsService : IStatsService
    {
        private readonly MoviesDbContext data;

        public StatsService(MoviesDbContext data) 
            => this.data = data;

        public StatsServiceModel Total()
        {
            var totalMovies = this.data.Movies.Count();
            var totalUsers = this.data.Users.Count();

            return new StatsServiceModel
            {
                TotalMovies = totalMovies,
                TotalUsers = totalUsers
            };
        }
    }
}
