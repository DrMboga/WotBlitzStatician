using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using WotBlitzStatician.Logic;
using WotBlitzStatician.Model.Dto;

namespace WotBlitzStatician.Controllers
{
  public class GuestAccountCache
  {
    private const string CacheKeyTemplate = "guestAccount{0}";

    private readonly IStatisticsCollectorEngine _statisticsCollectorEngine;
    private readonly IStatisticsCollectorFactory _statisticsCollectorFactory;
    private readonly IMemoryCache _cache;

    public GuestAccountCache(
      IStatisticsCollectorEngine statisticsCollectorEngine,
      IStatisticsCollectorFactory statisticsCollectorFactory,
      IMemoryCache cache)
    {
      _statisticsCollectorEngine = statisticsCollectorEngine;
      _statisticsCollectorFactory = statisticsCollectorFactory;
      _cache = cache;
    }

    public async Task<GuestAccountInfo> ReadAccountInfoAndPutInCache(long accountId)
    {
      var guestAccountInfo = new GuestAccountInfo();
      await _statisticsCollectorEngine.Collect(
          _statisticsCollectorFactory.CreateCollector(accountId, guestAccountInfo));

      string cacheKey = string.Format(CacheKeyTemplate, accountId);
      _cache.Set<GuestAccountInfo>(cacheKey, guestAccountInfo, TimeSpan.FromMinutes(10));

      return guestAccountInfo;
    }

    public async Task<GuestAccountInfo> ReadAccountInfoFromCache(long accountId)
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
