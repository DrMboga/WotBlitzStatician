using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WotBlitzStatician.Data.DataAccessors;
using WotBlitzStatician.JwtSecurity;
using WotBlitzStatician.WotApiClient;

namespace WotBlitzStatician.Controllers
{
  [Authorize]
  [Route("api/[controller]")]
  public class DictionariesController : Controller
  {
    private readonly ILogger<DictionariesController> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IWargamingApiClient _wgApi;
    private readonly IBlitzStaticianDictionary _blitzStatisticsDictionary;
    private readonly ISecurityServise _securityService;

    public DictionariesController(
        ILogger<DictionariesController> logger,
        IHttpContextAccessor httpContextAccessor,
        IWargamingApiClient wgApi,
        IBlitzStaticianDictionary blitzStaticianDictionary,
        ISecurityServise securityServise)
    {
      _logger = logger;
      _httpContextAccessor = httpContextAccessor;
      _wgApi = wgApi;
      _blitzStatisticsDictionary = blitzStaticianDictionary;
      _securityService = securityServise;
    }

    [HttpGet("LoadDictionariesAndPicturesIfNeeded")]
    public async Task<IActionResult> LoadAllDictionariesIfNeeded()
    {
      _blitzStatisticsDictionary.CreateDatabase();

      var someDictionaryData = await _blitzStatisticsDictionary.GetVehiclesTires();
      if (someDictionaryData == null || someDictionaryData.Count == 0)
      {
        await SaveDictionaries();
        await SaveVehicles();
        await SaveAchievements();
        await DownloadAllImages();
      }
      return Ok();
    }

    [HttpGet("SaveDictionaries")]
    public async Task<IActionResult> SaveDictionaries()
    {
      (var languages,
      var nations,
      var vehicleTypes,
      var achievementSections,
      var clanRoles) = await _wgApi.GetStaticDictionariesAsync();

      await _blitzStatisticsDictionary.SaveDictionaries(
          languages,
          nations,
          vehicleTypes,
          achievementSections,
          clanRoles);

      return Ok();

    }

    [HttpGet("SaveVehicles")]
    public async Task<IActionResult> SaveVehicles()
    {
      var vehicles = await _wgApi.GetWotEncyclopediaVehiclesAsync();
      await _blitzStatisticsDictionary.SaveVehicles(vehicles);
      return Ok();
    }

    [HttpGet("SaveAchievements")]
    public async Task<IActionResult> SaveAchievements()
    {
      var achievements = await _wgApi.GetAchievementsDictionaryAsync();
      await _blitzStatisticsDictionary.SaveAchievements(achievements);
      return Ok();
    }

    [HttpGet("DownloadAllImages")]
    public async Task<IActionResult> DownloadAllImages()
    {
      var allImages = await _blitzStatisticsDictionary.GetAllImages();

      _wgApi.DowloadImagesFromWg(allImages);

      return Ok();
    }

    [AllowAnonymous]
    [HttpPost("Authenticate")]
    public IActionResult Authenticate([FromBody] string secureWord)
    {
      var token = _securityService.Authenticate(secureWord);
      var remoteIpAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress;
      if (string.IsNullOrEmpty(token))
      {
        _logger.LogWarning($"Authentication denied for reqest from ip '{remoteIpAddress}'");
      }
      else
      {
        _logger.LogInformation($"Authentication granted for request from ip '{remoteIpAddress}'");
      }
      return Ok(token);
    }
  }
}
