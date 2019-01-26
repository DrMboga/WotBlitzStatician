﻿using System.Linq;
using System.Threading.Tasks;
using WotBlitzStatician.Logic.StatisticsCollectorOperations.OperationContext;

namespace WotBlitzStatician.Logic.StatisticsCollectorOperations.Operations
{
	public class FilterTanksByLastSessionOperation : IStatisticsCollectorOperation
	{
		#pragma warning disable CS1998
		public async Task Execute(StatisticsCollectorOperationContext operationContext)
		{
			foreach (var account in operationContext.Accounts)
			{
				var tanksNotPlayedLastSession = account.AccountInfoTanks
					.Where(t => t.LastBattleTime <= account.CurrentAccountInfo.LastBattleTime)
					.ToList();
				tanksNotPlayedLastSession.ForEach(t => account.AccountInfoTanks.Remove(t));
			}
		}
		#pragma warning restore CS1998
	}
}
