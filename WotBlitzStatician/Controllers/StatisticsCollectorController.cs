using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WotBlitzStatician.Logic;

namespace WotBlitzStatician.Controllers
{
    [Route("api/[controller]")]
    public class StatisticsCollectorController : Controller
    {
        private readonly IStatisticsCollectorEngine _statisticsCollectorEngine;
        private readonly IStatisticsCollectorFactory _statisticsCollectorFactory;

        public StatisticsCollectorController(
            IStatisticsCollectorEngine statisticsCollectorEngine,
            IStatisticsCollectorFactory statisticsCollectorFactory)
        {
            _statisticsCollectorEngine = statisticsCollectorEngine;
            _statisticsCollectorFactory = statisticsCollectorFactory;
        }

        [HttpGet()]
        public async Task<IActionResult> Index()
        {
            await _statisticsCollectorEngine.Collect(_statisticsCollectorFactory.CreateCollector());
            return Ok();
        }

        [HttpGet("{accountId}")]
        public async Task<IActionResult> CollectStatisticsByAccount(long accountId)
        {
            await _statisticsCollectorEngine.Collect(_statisticsCollectorFactory.CreateCollector(accountId));
            return Ok();
        }
    }
}
