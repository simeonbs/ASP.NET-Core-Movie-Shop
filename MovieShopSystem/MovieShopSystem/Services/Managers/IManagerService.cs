using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopSystem.Services.Managers
{
    public interface IManagerService
    {
        public bool IsManager(string userId);
    }
}
