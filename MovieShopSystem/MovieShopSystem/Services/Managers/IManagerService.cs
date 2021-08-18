using MovieShopSystem.Data.Models;

namespace MovieShopSystem.Services.Managers
{
    public interface IManagerService
    {
        public bool IsManager(string userId);

        public bool AddManager(Manager manager);

        public Manager GetManager(string id);
    }
}
