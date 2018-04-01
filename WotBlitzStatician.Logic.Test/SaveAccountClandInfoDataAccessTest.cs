using System;
using System.Threading.Tasks;
using Autofac;
using Microsoft.EntityFrameworkCore;
using WotBlitzStatician.Data;
using WotBlitzStatician.Data.DataAccessors;
using WotBlitzStatician.Model;
using Xunit;

namespace WotBlitzStatician.Logic.Test
{
	public class SaveAccountClandInfoDataAccessTest : IDisposable
	{
		private readonly IContainer _container;
		private readonly BlitzStaticianDbContext _dbContext;

		public SaveAccountClandInfoDataAccessTest()
		{
			var containerBuilder = new ContainerBuilder();

			containerBuilder.AddInMemoryDataBase();

			_container = containerBuilder.Build();
			_dbContext = _container.Resolve<BlitzStaticianDbContext>();
		}

		[Theory]
		[InlineData(1, 0, 1, null, "First player")]
		[InlineData(2, 2, 0, "Db player 1", null)]
		[InlineData(3, 3, 4, "Db player 2", "Wg player 2")]
		public async Task SaveAccountClanInfoTest(
			long accountId,
			long dbClanId,
			long wgClanId,
			string dbPlayerRole,
			string wgPlayerRole)
		{
			var accountDataAccessor = _container.Resolve<IAccountDataAccessor>();

			if(dbClanId > 0)
			{
				var dbAccountClanInfo = new AccountClanInfo
				{
					AccountInfo = new AccountInfo { AccountId = accountId },
					ClanId = dbClanId,
					PlayerRole = dbPlayerRole
				};
				await _dbContext.AccountClanInfo.AddAsync(dbAccountClanInfo);
				await _dbContext.SaveChangesAsync();
				_dbContext.Entry(dbAccountClanInfo.AccountInfo).State = EntityState.Detached;
				_dbContext.Entry(dbAccountClanInfo).State = EntityState.Detached;
			}

			var wgAccountClanInfo = new AccountClanInfo
			{
				AccountInfo = new AccountInfo { AccountId = accountId },
				ClanId = wgClanId,
				PlayerRole = wgPlayerRole
			};

			await accountDataAccessor.SaveAccountClanInfoAsync(accountId, wgAccountClanInfo);

			var savedClanInfo = await _dbContext.AccountClanInfo
				.SingleOrDefaultAsync(c => c.AccountInfo.AccountId == accountId);

			if (wgClanId > 0)
			{
				Assert.NotNull(savedClanInfo);
				Assert.Equal(wgClanId, savedClanInfo.ClanId);
				Assert.Equal(wgPlayerRole, savedClanInfo.PlayerRole);
			}
			else
			{
				Assert.Null(savedClanInfo);
			}
		}

		public void Dispose()
		{
			_dbContext.Dispose();
			_container.Dispose();
		}
	}
}
