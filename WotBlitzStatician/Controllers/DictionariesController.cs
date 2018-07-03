using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WotBlitzStatician.Data.DataAccessors;
using WotBlitzStatician.WotApiClient;

namespace WotBlitzStatician.Controllers
{
  [Route("api/[controller]")]
  public class DictionariesController : Controller
  {
    private readonly IWargamingApiClient _wgApi;
    private readonly IBlitzStaticianDictionary _blitzStatisticsDictionary;

    public DictionariesController(IWargamingApiClient wgApi, IBlitzStaticianDictionary blitzStaticianDictionary)
    {
      _wgApi = wgApi;
      _blitzStatisticsDictionary = blitzStaticianDictionary;
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

  }
}
