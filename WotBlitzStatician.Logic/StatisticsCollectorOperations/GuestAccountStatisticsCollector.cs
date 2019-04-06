using System;
using Autofac;
using WotBlitzStatician.Data.DataAccessors;
using WotBlitzStatician.Logic.StatisticsCollectorOperations.Operations;
using WotBlitzStatician.Model;
using WotBlitzStatician.Model.Dto;
using WotBlitzStatician.WotApiClient;

namespace WotBlitzStatician.Logic.StatisticsCollectorOperations
{
  public class GuestAccountStatisticsCollector : IStatisticsCollector
  {
    private readonly ILifetimeScope _scope;
    private readonly long _accountId;

    private readonly GuestAccountInfo _guestAccountInfo;
    public GuestAccountStatisticsCollector(ILifetimeScope scope,
          GuestAccountInfo guestAccountInfo,
          long accountId)
    {
      _accountId = accountId;
      (_scope, OperationsCount) = CreateOperationSteps(scope);
      _guestAccountInfo = guestAccountInfo;
    }

    public int OperationsCount { get; }

    public IStatisticsCollectorOperation GetOperation(int index)
    {
      if (index < 0 || index > OperationsCount)
        throw new IndexOutOfRangeException($"Index {index} must be between 0 and {OperationsCount}");

      return _scope.ResolveKeyed<IStatisticsCollectorOperation>(index,
          new NamedParameter("pAccountId", _accountId),
          new NamedParameter("pGuestAccountInfoToFill", _guestAccountInfo));
    }

    private static (ILifetimeScope, int) CreateOperationSteps(ILifetimeScope lifetimeScope)
    {
      var childScope = lifetimeScope.BeginLifetimeScope(builder =>
      {
        builder.Register((c, p) => new SetGuestAccountInfoToCollect(
                p.Named<long>("pAccountId")
             ))
            .Keyed<IStatisticsCollectorOperation>(0);
        builder.RegisterType<GetAccountStatistics>()
            .Keyed<IStatisticsCollectorOperation>(1);
        builder.RegisterType<GetAccountsClanInfoOperation>()
            .Keyed<IStatisticsCollectorOperation>(2);
        builder.RegisterType<GetAccountsAchievementsOperation>()
            .Keyed<IStatisticsCollectorOperation>(3);
        builder.RegisterType<GetAllTanksStatisticsOperation>()
            .Keyed<IStatisticsCollectorOperation>(4);
        builder.RegisterType<CalculateMiddleTierOperation>()
            .Keyed<IStatisticsCollectorOperation>(5);
        builder.RegisterType<CalculateWn7Operation>()
            .Keyed<IStatisticsCollectorOperation>(6);
        builder.RegisterType<GetTanksAchievementsOperation>()
            .Keyed<IStatisticsCollectorOperation>(7);
        builder.Register((c, p) => new BuildGuestAccountInfoOperation(
                p.Named<GuestAccountInfo>("pGuestAccountInfoToFill"),
                c.Resolve<IAccountsTankInfoDataAccessor>(),
                c.Resolve<IBlitzStaticianDictionary>()
             ))
            .Keyed<IStatisticsCollectorOperation>(8);
      });

      // ToDo: Increase this number if adding new operation to scope.
      // It's an autofac restriction - We can't iterate through IIndex<IStatisticsCollectorOperation, int>
      int operationsCount = 9;

      return (childScope, operationsCount);
    }

    #region IDisposable Support
    private bool disposedValue = false; // To detect redundant calls

    protected virtual void Dispose(bool disposing)
    {
      if (!disposedValue)
      {
        if (disposing)
        {
          _scope.Dispose();
        }
        disposedValue = true;
      }
    }

    public void Dispose()
    {
      Dispose(true);
    }
    #endregion

  }
}