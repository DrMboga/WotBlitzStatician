using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
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
        public IEnumerable<string> Get()
        {
			// GetStaticDictionariesAsync
            return new string[] { "value1", "value2" };
        }

    }
}
