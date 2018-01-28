using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WotBlitzStatician.Model;
using WotBlitzStatician.WotApiClient;

namespace WotBlitzStatician.Controllers
{
	[Route("api/[controller]")]
    public class DictionariesController : Controller
    {
		private readonly IWargamingApiClient _wgApi;
		private readonly ILogger<DictionariesController> _logger;


		public DictionariesController(IWargamingApiClient wgApi, ILogger<DictionariesController> logger)
		{
			_wgApi = wgApi;
			_logger = logger;
		}


        // GET: api/<controller>
        [HttpGet]
        public async Task<IEnumerable<DictionaryLanguage>> Get()
        {
			_logger.LogInformation("Hi from controller");
			(var languages,
			var nations,
			var vehicleTypes,
			var achievementSections,
			var clanRoles) = await _wgApi.GetStaticDictionariesAsync();

			return languages;

		}

    }
}
