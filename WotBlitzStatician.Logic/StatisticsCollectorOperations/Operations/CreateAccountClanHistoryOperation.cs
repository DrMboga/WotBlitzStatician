using System;
using System.Threading.Tasks;
using WotBlitzStatician.Data.DataAccessors;
using WotBlitzStatician.Logic.StatisticsCollectorOperations.OperationContext;

namespace WotBlitzStatician.Logic.StatisticsCollectorOperations.Operations
{
	public class CreateAccountClanHistoryOperation : IStatisticsCollectorOperation
	{
		private readonly IAccountDataAccessor _accountDataAccessor;

		public CreateAccountClanHistoryOperation(IAccountDataAccessor accountDataAccessor)
		{
			_accountDataAccessor = accountDataAccessor;
		}

		public async Task Execute(StatisticsCollectorOperationContext operationContext)
		{
			foreach (var accountInfo in operationContext.Accounts)
			{
				accountInfo.AccountClanHistory = null;
				var existingAccountClanInfo = await _accountDataAccessor
					.GetAccountClanAsync(accountInfo.CurrentAccountInfo.AccountId);

				if(existingAccountClanInfo == null && 
					(accountInfo.WargamingAccountInfo.AccountClanInfo == null
						|| accountInfo.WargamingAccountInfo.AccountClanInfo.ClanId == 0))
				{
					return;
				}

				if(existingAccountClanInfo == null && 
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

				if(existingAccountClanInfo != null &&
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
