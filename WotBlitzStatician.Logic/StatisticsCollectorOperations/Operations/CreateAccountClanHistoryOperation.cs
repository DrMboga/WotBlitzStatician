using System;
using System.Threading.Tasks;
using WotBlitzStatician.Data.DataAccessors;
using WotBlitzStatician.Logic.StatisticsCollectorOperations.OperationContext;

namespace WotBlitzStatician.Logic.StatisticsCollectorOperations.Operations
{
  public class CreateAccountClanHistoryOperation : IStatisticsCollectorOperation
  {
    private readonly IAccountDataAccessor _accountDataAccessor;
    private readonly IClanInfoDataAccessor _clanInfoDataAccessor;

    public CreateAccountClanHistoryOperation(
      IAccountDataAccessor accountDataAccessor,
      IClanInfoDataAccessor clanInfoDataAccessor
      )
    {
      _accountDataAccessor = accountDataAccessor;
      _clanInfoDataAccessor = clanInfoDataAccessor;
    }

    public async Task Execute(StatisticsCollectorOperationContext operationContext)
    {
      foreach (var accountInfo in operationContext.Accounts)
      {
        accountInfo.AccountClanHistory = null;
        var existingAccountClanInfo = await _clanInfoDataAccessor
          .GetAccountClanAsync(accountInfo.CurrentAccountInfo.AccountId);

        operationContext.OperationStateMessage =
        $"For account {accountInfo.CurrentAccountInfo.AccountId} existingAccountClanId {(existingAccountClanInfo == null ? "null" : existingAccountClanInfo.ClanId.ToString())}; ";

        operationContext.OperationStateMessage +=
        $"WargamingAccountInfo.ClanId {(accountInfo.WargamingAccountInfo.AccountClanInfo == null ? "null" : accountInfo.WargamingAccountInfo.AccountClanInfo.ClanId.ToString())}; ";

        if (existingAccountClanInfo == null &&
          (accountInfo.WargamingAccountInfo.AccountClanInfo == null
            || accountInfo.WargamingAccountInfo.AccountClanInfo.ClanId == 0))
        {
          operationContext.OperationStateMessage += "Notning changes";
          return;
        }

        operationContext.OperationStateMessage += "Adding clan history operation";


        if (existingAccountClanInfo == null &&
          accountInfo.WargamingAccountInfo.AccountClanInfo != null &&
          accountInfo.WargamingAccountInfo.AccountClanInfo.ClanId != 0)
        {
          accountInfo.AccountClanHistory = new Model.AccountClanHistory
          {
            AccountId = accountInfo.CurrentAccountInfo.AccountId,
            ClanId = accountInfo.WargamingAccountInfo.AccountClanInfo.ClanId,
            ClanName = accountInfo.WargamingAccountInfo.AccountClanInfo.ClanName,
            ClanTag = accountInfo.WargamingAccountInfo.AccountClanInfo.ClanTag,
            PlayerJoinedAt = accountInfo.WargamingAccountInfo.AccountClanInfo.PlayerJoinedAt,
            PlayerRole = accountInfo.WargamingAccountInfo.AccountClanInfo.PlayerRole
          };
          return;
        }

        if (existingAccountClanInfo != null &&
          (accountInfo.WargamingAccountInfo.AccountClanInfo == null
            || accountInfo.WargamingAccountInfo.AccountClanInfo.ClanId == 0))
        {
          accountInfo.AccountClanHistory = new Model.AccountClanHistory
          {
            AccountId = accountInfo.CurrentAccountInfo.AccountId,
            ClanId = null,
            ClanName = null,
            ClanTag = null,
            PlayerJoinedAt = DateTime.Now,
            PlayerRole = null
          };
          return;
        }

        if (existingAccountClanInfo != null &&
          accountInfo.WargamingAccountInfo.AccountClanInfo != null &&
          accountInfo.WargamingAccountInfo.AccountClanInfo.ClanId != 0 &&
          (existingAccountClanInfo.ClanId != accountInfo.WargamingAccountInfo.AccountClanInfo.ClanId
            || existingAccountClanInfo.PlayerRole != accountInfo.WargamingAccountInfo.AccountClanInfo.PlayerRole))
        {
          accountInfo.AccountClanHistory = new Model.AccountClanHistory
          {
            AccountId = accountInfo.CurrentAccountInfo.AccountId,
            ClanId = accountInfo.WargamingAccountInfo.AccountClanInfo.ClanId,
            ClanName = accountInfo.WargamingAccountInfo.AccountClanInfo.ClanName,
            ClanTag = accountInfo.WargamingAccountInfo.AccountClanInfo.ClanTag,
            PlayerJoinedAt = accountInfo.WargamingAccountInfo.AccountClanInfo.PlayerJoinedAt,
            PlayerRole = accountInfo.WargamingAccountInfo.AccountClanInfo.PlayerRole
          };
        }
      }
    }
  }
}
