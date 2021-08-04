using MovieShopSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
