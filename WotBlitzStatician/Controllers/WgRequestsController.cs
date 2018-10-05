using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WotBlitzStatician.WotApiClient;

namespace WotBlitzStatician.Controllers
{
    [Route("api/[controller]")]
    public class WgRequestsController : Controller
    {
        private readonly IWgApiConfiguration _appSettings;

        public WgRequestsController(IWgApiConfiguration appsettings)
        {
            _appSettings = appsettings;
        }

        [HttpGet("Authentication")]
        public IActionResult GetAuthenticationRequest()
        {
            // https://api.worldoftanks.ru/wot/auth/login/?application_id=e6bb7a6cd1b7da3ca5697f97072d2176&redirect_uri=http://localhost:58249/account/90277267
            return Ok($"{_appSettings.WotBaseAddress}auth/login/?application_id={_appSettings.ApplicationId}");
        }
    }
}