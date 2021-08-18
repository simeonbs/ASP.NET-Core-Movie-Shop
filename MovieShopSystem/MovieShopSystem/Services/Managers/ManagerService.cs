using MovieShopSystem.Data;
using System.Linq;

namespace MovieShopSystem.Services.Managers
{
    public class ManagerService : IManagerService
    {
        private readonly MoviesDbContext data;

        public ManagerService(MoviesDbContext data) 
            => this.data = data;

        public bool IsManager(string userId)
                => this
                    .data
                    .Managers
                    .Any(m => m.UserId == userId);
    }
}
