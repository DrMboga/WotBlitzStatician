using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WotBlitzStatician.Data.DataAccessors;
using WotBlitzStatician.Model;

namespace WotBlitzStatician.Controllers
{
  [Route("api/[controller]")]
  public class TanksStatController : Controller
  {
    private readonly IAccountsTankInfoDataAccessor _accountsTankInfoDataAccessor;

    public TanksStatController(
    IAccountsTankInfoDataAccessor accountsTankInfoDataAccessor)
    {
      _accountsTankInfoDataAccessor = accountsTankInfoDataAccessor;
    }

    // api/TanksStat/TanksByAchievement/90277267?achievementId=warrior
    [HttpGet("TanksByAchievement/{accountId}")]
    public async Task<IActionResult> GetTanksByAchievement(long accountId, [FromQuery] string achievementId)
    {
      var tanks = await _accountsTankInfoDataAccessor.GetAllTanksByAchievement(accountId, achievementId);
      return Ok(tanks);
    }

    // api/TanksStat/AggregatedAccountTanksInfo/90277267
    [HttpGet("AggregatedAccountTanksInfo/{accountId}")]
    public async Task<IActionResult> GetAggregatedAccountTanksInfo(long accountId)
    {
      var aggregatedAccountInfo = await _accountsTankInfoDataAccessor.GetAggregatedAccountTanksInfo(accountId);
      return Ok(aggregatedAccountInfo);
    }
  }
}
