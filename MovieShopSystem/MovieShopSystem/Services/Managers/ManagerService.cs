using MovieShopSystem.Data;
using MovieShopSystem.Data.Models;
using System.Linq;

namespace MovieShopSystem.Services.Managers
{
    public class ManagerService : IManagerService
    {
        private readonly MoviesDbContext data;

        public ManagerService(MoviesDbContext data)
            => this.data = data;

        public bool AddManager(Manager manager)
        {
            this.data.Managers.Add(manager);
            this.data.SaveChanges();
            return true;
        }

        public bool IsManager(string userId)
                => this
                    .data
                    .Managers
                    .Any(m => m.UserId == userId);

        public Manager GetManager(string id)
            => this.data.Managers.Where(m => m.UserId == id).FirstOrDefault();
    }
}
