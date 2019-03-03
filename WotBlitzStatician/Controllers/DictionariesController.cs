using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WotBlitzStatician.Data.DataAccessors;
using WotBlitzStatician.JwtSecurity;
using WotBlitzStatician.WotApiClient;

namespace WotBlitzStatician.Controllers
{
  [Authorize]
  [Route("api/[controller]")]
  public class DictionariesController : Controller
  {
    private readonly IWargamingApiClient _wgApi;
    private readonly IBlitzStaticianDictionary _blitzStatisticsDictionary;
    private readonly ISecurityServise _securityService;

    public DictionariesController(IWargamingApiClient wgApi,
        IBlitzStaticianDictionary blitzStaticianDictionary,
        ISecurityServise securityServise)
    {
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
      return Ok(_securityService.Authenticate(secureWord));
    }
  }
}
