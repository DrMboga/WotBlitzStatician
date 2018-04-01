using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Microsoft.EntityFrameworkCore;
using WotBlitzStatician.Data;
using WotBlitzStatician.Logic.StatisticsCollectorOperations.OperationContext;
using WotBlitzStatician.Logic.StatisticsCollectorOperations.Operations;
using WotBlitzStatician.Model;
using Xunit;

namespace WotBlitzStatician.Logic.Test
{
	public class AccountClanInfoHistoryOperationTest : IDisposable
	{
		private readonly IContainer _container;
		private readonly BlitzStaticianDbContext _dbContext;

		public AccountClanInfoHistoryOperationTest()
		{
			var containerBuilder = new ContainerBuilder();

			containerBuilder.AddInMemoryDataBase();
			containerBuilder.RegisterType<CreateAccountClanHistoryOperation>().AsSelf();

			_container = containerBuilder.Build();
			_dbContext = _container.Resolve<BlitzStaticianDbContext>();
		}

		[Theory]
		[InlineData(1, 0, 15, null, "SuperPlayer", true)]
		[InlineData(2, 12, 0, "player 1", null, true)]
		[InlineData(3, 10, 14, "player 1", "player 1", true)]
		[InlineData(4, 10, 10, "player 1", "player 2", true)]
		[InlineData(5, 16, 17, "player 1", "player 2", true)]
		[InlineData(6, 25, 25, "player 1", "player 1", false)]
		[InlineData(7, 0, 0, null, null, false)]
		public async Task CreateClanHistoryOperationTest(
			long accountId,
			int databaseClanId, 
			int wgClanId,
			string databasePlayerRole,
			string wgPlayerRole,
			bool historyMustBeCreated)
		{
			var operation = _container.Resolve<CreateAccountClanHistoryOperation>();
			// FillingDatabase
			if(databaseClanId > 0)
			{
				var clanInfo = new AccountClanInfo
				{
					AccountInfo = new AccountInfo { AccountId = accountId },
					ClanId = databaseClanId,
					PlayerRole = databasePlayerRole
				};
				await _dbContext.AccountInfo.AddAsync(clanInfo.AccountInfo);
				await _dbContext.AccountClanInfo.AddAsync(clanInfo);
				await _dbContext.SaveChangesAsync();
				_dbContext.Entry(clanInfo.AccountInfo).State = EntityState.Detached;
				_dbContext.Entry(clanInfo).State = EntityState.Detached;
			}
			var operationContext = new StatisticsCollectorOperationContext
			{
				Accounts = new List<SatisticsCollectorAccountOperationContext>
				{
					new SatisticsCollectorAccountOperationContext
					{
						WargamingAccountInfo = new AccountInfo
						{
							AccountId = accountId,
							AccountClanInfo = new AccountClanInfo
							{
								ClanId = wgClanId,
								PlayerRole = wgPlayerRole
							}
						},
						CurrentAccountInfo = new AccountInfo
						{
							AccountId = accountId
						}
					}
				}
			};

			await operation.Execute(operationContext);
			var history = operationContext.Accounts.Single().AccountClanHistory;

			if (historyMustBeCreated)
			{
				Assert.NotNull(history);
				Assert.Equal(wgClanId, history.ClanId ?? 0);
				Assert.Equal(wgPlayerRole, history.PlayerRole);
			}
			else
			{
				Assert.Null(history);
			}
		}

		public void Dispose()
		{
			_dbContext.Dispose();
			_container.Dispose();
		}
	}
}
