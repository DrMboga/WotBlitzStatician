using Autofac;
using WotBlitzStatician.Logic.StatisticsCollectorOperations;
using WotBlitzStatician.Model.Dto;

namespace WotBlitzStatician.Logic
{
  public class StatisticsCollectorFactory : IStatisticsCollectorFactory
  {
    private readonly ILifetimeScope _childScope;

    public StatisticsCollectorFactory(ILifetimeScope childScope)
    {
      _childScope = childScope;
    }

    public IStatisticsCollector CreateCollector(long? accountId = null)
    {
      if (accountId.HasValue)
      {
        return new OneAccountStatisticsCollector(_childScope, accountId.Value);
      }
      return new AllAccountStatisticsCollector(_childScope);
    }

    public IStatisticsCollector CreateCollector(long accountId, GuestAccountInfo guestAccountInfo)
    {
      return new GuestAccountStatisticsCollector(_childScope, guestAccountInfo, accountId);
    }
  }
}