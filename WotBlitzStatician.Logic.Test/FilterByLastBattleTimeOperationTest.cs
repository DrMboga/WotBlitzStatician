namespace WotBlitzStatician.Logic.Test
{
	using System;
	using System.Linq;
	using System.Threading.Tasks;
	using Autofac;
	using WotBlitzStatician.Logic.StatisticsCollectorOperations.OperationContext;
	using WotBlitzStatician.Logic.StatisticsCollectorOperations.Operations;
	using WotBlitzStatician.Model;
	using Xunit;

	public class FilterByLastBattleTimeOperationTest
	{
		private readonly IContainer _container;
		private readonly StatisticsCollectorOperationContext _operationContext;

		private readonly DateTime _notChangingBattleLifeTime;
		private readonly DateTime _previousBattleLifeTimeFromDb;
		private readonly DateTime _newBattleLifeTimeFromWg;



		public FilterByLastBattleTimeOperationTest()
		{
			_notChangingBattleLifeTime = DateTime.Now.AddDays(-7);
			_previousBattleLifeTimeFromDb = DateTime.Now.AddDays(-2);
			_newBattleLifeTimeFromWg = DateTime.Now.AddHours(-1);
			_operationContext = PrepareOperationContextForProlongAccessToken();

			var containerBuilder = new ContainerBuilder();
			containerBuilder.RegisterType<FilterByLastBattleTimeOperation>().AsSelf();
			_container = containerBuilder.Build();
		}

		[Fact]
		public async Task FilterOperationTest()
		{
			var filterByLastBattleTimeOperation = _container.Resolve<FilterByLastBattleTimeOperation>();
			await filterByLastBattleTimeOperation.Execute(_operationContext);

			Assert.Single(_operationContext.Accounts);
			Assert.Equal(_previousBattleLifeTimeFromDb, _operationContext.Accounts.Single().CurrentAccountInfo.LastBattleTime);
			Assert.Equal(_newBattleLifeTimeFromWg, _operationContext.Accounts.Single().WargamingAccountInfo.LastBattleTime);
		}

		private StatisticsCollectorOperationContext PrepareOperationContextForProlongAccessToken()
		{
			var operationContext = new StatisticsCollectorOperationContext();

			operationContext.Accounts.Add(new SatisticsCollectorAccountOperationContext
			{
				CurrentAccountInfo = new AccountInfo
				{
					LastBattleTime = _notChangingBattleLifeTime
				},
				WargamingAccountInfo = new AccountInfo()
				{
					LastBattleTime = _notChangingBattleLifeTime
				}
			});
			operationContext.Accounts.Add(new SatisticsCollectorAccountOperationContext
			{
				CurrentAccountInfo = new AccountInfo
				{
					LastBattleTime = _previousBattleLifeTimeFromDb
				},
				WargamingAccountInfo = new AccountInfo()
				{
					LastBattleTime = _newBattleLifeTimeFromWg
				}
			});

			return operationContext;
		}

	}
}