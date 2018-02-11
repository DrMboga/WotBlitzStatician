using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WotBlitzStatician.Controllers
{
	[Route("api/[controller]")]
	public class StatisticsCollectorController : Controller
    {
		[HttpGet()]
		public async Task<IActionResult> Index()
        {
			// ToDo: Logic for read accounts info and get and save all statistics from WG 
			// by operation tasks and flow
			return Ok();
		}
	}
}