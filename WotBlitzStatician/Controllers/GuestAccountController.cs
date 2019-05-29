using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using WotBlitzStatician.Logic;
using WotBlitzStatician.Model.Dto;

namespace WotBlitzStatician.Controllers
{
  [Route("api/[controller]")]

  public class GuestAccountController : Controller
  {
    private GuestAccountCache _guestAccountCache;

    public GuestAccountController(
      GuestAccountCache guestAccountCache)
    {
      _guestAccountCache = guestAccountCache;
    }

    // api/GuestAccount/accountId
    [HttpPut("{accountId}")]
    public async Task<IActionResult> UpdateAccountInfo(long accountId)
    {
      var guestAccountInfo = await _guestAccountCache.ReadAccountInfoAndPutInCache(accountId);
      return Ok();
    }

    // api/GuestAccount/{accountId}/accountinfo
    [HttpGet("{accountId}/accountinfo")]
    public async Task<IActionResult> GetCachedAccountInfo(long accountId)
    {
      var accountInfo = await _guestAccountCache.ReadAccountInfoFromCache(accountId);
      return Ok(accountInfo.AccountInfo);
    }

    // api/GuestAccount/{accountId}/aggregatedaccountInfo
    [HttpGet("{accountId}/aggregatedaccountInfo")]
    public async Task<IActionResult> GetCachedAggregatedAccountInfo(long accountId)
    {
      var accountInfo = await _guestAccountCache.ReadAccountInfoFromCache(accountId);
      return Ok(accountInfo.AggregatedAccountInfo);
    }

    // api/GuestAccount/{accountId}/achievements
    [HttpGet("{accountId}/achievements")]
    public async Task<IActionResult> GetCachedAchievementsAccountInfo(long accountId)
    {
      var accountInfo = await _guestAccountCache.ReadAccountInfoFromCache(accountId);
      return Ok(accountInfo.Achievements);
    }

    // api/GuestAccount/{accountId}/tanks
    [HttpGet("{accountId}/tanks")]
    public async Task<IActionResult> GetCachedTanks(long accountId)
    {
      var accountInfo = await _guestAccountCache.ReadAccountInfoFromCache(accountId);
      return Ok(accountInfo.Tanks);
    }
  }
}
