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

				if(existingAccountClanInfo == null && accountInfo.WargamingAccountInfo.AccountClanInfo != null)
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

				// ToDo: Check if existing not null and current null
				// If both not null and clan id is differ
				// If both not null and player role is differ

				// ToDo: unit test this logic with in-memory database and method GetAccountClanAsync
				// ToDo: unit test SaveAccountClanInfoAsync with in-memery database
			}
		}
	}
}
