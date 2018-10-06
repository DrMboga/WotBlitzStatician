using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WotBlitzStatician.WotApiClient;
using WotBlitzStatician.WotApiClient.RequestStringBuilder;

namespace WotBlitzStatician.Controllers
{
    [Route("api/[controller]")]
    public class WgRequestsController : Controller
    {
        private readonly IRequestBuilder _requestBuilder;

        public WgRequestsController(IRequestBuilder requestBuilder)
        {
            _requestBuilder = requestBuilder;
        }

        [HttpGet("Authentication")]
        public IActionResult GetAuthenticationRequest([FromQuery] string redirectUrl)
        {
			string requestUrl =	_requestBuilder.BuildRequestUrl(
					RequestType.Login,
					new RequestParameter { ParameterType = ParameterType.RedirectUri, ParameterValue = redirectUrl }
					);

            var authUrl = new Uri($"{_requestBuilder.WotBaseAddress}{requestUrl}");
            return Ok(authUrl);
        }
    }
}