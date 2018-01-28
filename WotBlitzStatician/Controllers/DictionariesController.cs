using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WotBlitzStatician.Model;
using WotBlitzStatician.WotApiClient;

namespace WotBlitzStatician.Controllers
{
	[Route("api/[controller]")]
    public class DictionariesController : Controller
    {
		private readonly IWargamingApiClient _wgApi;

		public DictionariesController(IWargamingApiClient wgApi)
		{
			_wgApi = wgApi;
		}


        // GET: api/<controller>
        [HttpGet]
        public async Task<IEnumerable<DictionaryLanguage>> Get()
        {
			(var languages,
			var nations,
			var vehicleTypes,
			var achievementSections,
			var clanRoles) = await _wgApi.GetStaticDictionariesAsync();

			return languages;

		}

    }
}
