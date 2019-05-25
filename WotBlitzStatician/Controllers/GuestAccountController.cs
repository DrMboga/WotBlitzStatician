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
    private const string CacheKeyTemplate = "guestAccount{0}";

    private readonly IStatisticsCollectorEngine _statisticsCollectorEngine;
    private readonly IStatisticsCollectorFactory _statisticsCollectorFactory;
    private readonly IMemoryCache _cache;


    public GuestAccountController(
      IStatisticsCollectorEngine statisticsCollectorEngine,
      IStatisticsCollectorFactory statisticsCollectorFactory,
      IMemoryCache cache)
    {
      _statisticsCollectorEngine = statisticsCollectorEngine;
      _statisticsCollectorFactory = statisticsCollectorFactory;
      _cache = cache;
    }

    // api/GuestAccount/accountId
    [HttpPut("{accountId}")]
    public async Task<IActionResult> UpdateAccountInfo(long accountId)
    {
      var guestAccountInfo = await ReadAccountInfoAndPutInCache(accountId);
      return Ok();
    }

    // api/GuestAccount/{accountId}/accountinfo
    [HttpGet("{accountId}/accountinfo")]
    public async Task<IActionResult> GetCachedAccountInfo(long accountId)
    {
      var accountInfo = await ReadAccountInfoFromCache(accountId);
      return Ok(accountInfo.AccountInfo);
    }

    // api/GuestAccount/{accountId}/aggregatedaccountInfo
    [HttpGet("{accountId}/aggregatedaccountInfo")]
    public async Task<IActionResult> GetCachedAggregatedAccountInfo(long accountId)
    {
      var accountInfo = await ReadAccountInfoFromCache(accountId);
      return Ok(accountInfo.AggregatedAccountInfo);
    }

    // api/GuestAccount/{accountId}/achievements
    [HttpGet("{accountId}/achievements")]
    public async Task<IActionResult> GetCachedAchievementsAccountInfo(long accountId)
    {
      var accountInfo = await ReadAccountInfoFromCache(accountId);
      return Ok(accountInfo.Achievements);
    }

// ToDo: Make an Odata
    // api/GuestAccount/{accountId}/tanks
    [HttpGet("{accountId}/tanks")]
    public async Task<IActionResult> GetCachedTanks(long accountId)
    {
      var accountInfo = await ReadAccountInfoFromCache(accountId);
      return Ok(accountInfo.Tanks);
    }

    private async Task<GuestAccountInfo> ReadAccountInfoAndPutInCache(long accountId)
    {
      var guestAccountInfo = new GuestAccountInfo();
      await _statisticsCollectorEngine.Collect(
          _statisticsCollectorFactory.CreateCollector(accountId, guestAccountInfo));

      string cacheKey = string.Format(CacheKeyTemplate, accountId);
      _cache.Set<GuestAccountInfo>(cacheKey, guestAccountInfo, TimeSpan.FromMinutes(10));

      return guestAccountInfo;
    }

    private async Task<GuestAccountInfo> ReadAccountInfoFromCache(long accountId)
    {
      string cacheKey = string.Format(CacheKeyTemplate, accountId);
      GuestAccountInfo accountInfo;
      if (!_cache.TryGetValue<GuestAccountInfo>(cacheKey, out accountInfo))
      {
        accountInfo = await ReadAccountInfoAndPutInCache(accountId);
      }
      return accountInfo;
    }

  }
}
