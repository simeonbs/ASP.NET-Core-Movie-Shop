using Microsoft.AspNetCore.Mvc;
using MovieShopSystem.Data;
using MovieShopSystem.Services.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopSystem.Controllers.Api
{
    [ApiController]
    [Route("api/stats")]
    public class StatsApiController : ControllerBase
    {
        private readonly IStatsService stats;

        public StatsApiController(IStatsService stats)
            => this.stats = stats;

        [HttpGet]
        public StatsServiceModel Get()
        {
            return this.stats.Total();
        }
    }
}
