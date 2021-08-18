using Microsoft.AspNetCore.Mvc;
using MovieShopSystem.Services.Stats;

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
