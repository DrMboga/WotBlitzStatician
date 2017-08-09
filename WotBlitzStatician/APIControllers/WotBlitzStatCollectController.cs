namespace WotBlitzStatician.APIControllers
{
	using System.Threading.Tasks;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.Extensions.Logging;
	using WotBlitzStatician.Logic;

	[Route("api/[controller]")]
	public class WotBlitzStatCollectController : Controller
	{
		private readonly IBlitzStaticianLogic _blitzStaticianLogic;
		private readonly ILogger _logger;

		public WotBlitzStatCollectController(IBlitzStaticianLogic blitzStaticianLogic, ILogger<WotBlitzStatCollectController> logger)
		{
			_blitzStaticianLogic = blitzStaticianLogic;
			_logger = logger;
		}

		[HttpPut("{accountId}")]
		public async Task Put(long accountId)
		{
			_logger.LogDebug($"WotBlitzStatCollect: {accountId}");
			await _blitzStaticianLogic.LoadStatisticsFromWgAsync(accountId);
		}

		[HttpGet]
		public string GetStatus()
		{
			return "OK";
		}
	}
}
