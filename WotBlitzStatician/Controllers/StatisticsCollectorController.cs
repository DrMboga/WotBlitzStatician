using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WotBlitzStatician.Logic;

namespace WotBlitzStatician.Controllers
{
	[Route("api/[controller]")]
	public class StatisticsCollectorController : Controller
    {
		private readonly IStatisticsCollector _statisticsCollector;

		public StatisticsCollectorController(IStatisticsCollector statisticsCollector)
		{
			_statisticsCollector = statisticsCollector;
		}

		[HttpGet()]
		public async Task<IActionResult> Index()
        {
			await _statisticsCollector.CollectAllStatistics();
			return Ok();
		}
	}
}