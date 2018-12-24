using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using WotBlitzStatician.Data.DataAccessors;
using WotBlitzStatician.WotApiClient;
using WotBlitzStatician.WotApiClient.RequestStringBuilder;

namespace WotBlitzStatician.Controllers
{
    [Route("api/[controller]")]
    public class WgRequestsController : Controller
    {
        private readonly IRequestBuilder _requestBuilder;
        private readonly IAccountInfoViewDataAccessor _accountInfoViewDataAccessor;
        private readonly IWargamingApiClient _wargamingApiClient;
        private readonly ILogger<WgRequestsController> _logger;
        private readonly IMemoryCache _cache;

        public WgRequestsController(IRequestBuilder requestBuilder,
                IAccountInfoViewDataAccessor accountInfoViewDataAccessor,
                IWargamingApiClient wargamingApiClient,
                ILogger<WgRequestsController> logger,
                IMemoryCache cache)
        {
            _requestBuilder = requestBuilder;
            _accountInfoViewDataAccessor = accountInfoViewDataAccessor;
            _wargamingApiClient = wargamingApiClient;
            _logger = logger;
            _cache = cache;
        }

        [HttpGet("Authentication")]
        public IActionResult GetAuthenticationRequest([FromQuery] string redirectUrl)
        {
            string requestUrl = _requestBuilder.BuildRequestUrl(
                    RequestType.Login,
                    new RequestParameter { ParameterType = ParameterType.RedirectUri, ParameterValue = redirectUrl }
                    );

            var authUrl = new Uri($"{_requestBuilder.WotBaseAddress}{requestUrl}");
            return Ok(authUrl);
        }

        // api/WgRequests/AccountPrivateInfo/90277267
        [HttpGet("AccountPrivateInfo/{accountId}")]
        public async Task<IActionResult> GetAccountPrivateInfo(long accountId)
        {
            var accessToken = await _accountInfoViewDataAccessor.GetAccountAccessToken(accountId);
            if (string.IsNullOrEmpty(accessToken))
            {
                return Ok();
            }
            try
            {
                string cacheKey = $"privateInfo_{accessToken}";

                var privateInfo = await _cache.GetOrCreateAsync(cacheKey, entry =>
                {
                    entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(10)); // ToDo: Create config section!
                    return _wargamingApiClient.GetAccountPrivateInfo(accountId, accessToken);
                });
                return Ok(privateInfo);
            }
            catch (Exception ex)
            {
                _logger.LogWarning("GetAccountPrivateInfo error: {ex}", ex);
            }
            return Ok();
        }
    }
}