using System.Linq;
using System.Threading.Tasks;
using WotBlitzStatician.Data.DataAccessors;
using WotBlitzStatician.Logic.Calculations;
using WotBlitzStatician.Logic.StatisticsCollectorOperations.OperationContext;

namespace WotBlitzStatician.Logic.StatisticsCollectorOperations.Operations
{
	public class CalculateWn7Operation : IStatisticsCollectorOperation
	{
		private readonly IBlitzStaticianDictionary _blitzStaticianDictionary;

		public CalculateWn7Operation(IBlitzStaticianDictionary blitzStaticianDictionary)
		{
			_blitzStaticianDictionary = blitzStaticianDictionary;
		}


		public async Task Execute(StatisticsCollectorOperationContext operationContext)
		{
			var tankTires = await _blitzStaticianDictionary.GetVehiclesTires();

			foreach (var account in operationContext.Accounts)
			{
				account.WargamingAccountInfo.AccountInfoStatistics
					.Single()
					.CalculateWn7();
				account.AccountInfoTanks.ForEach(t => 
				{
					if (tankTires.ContainsKey(t.TankId))
						t.CalculateWn7(tankTires[t.TankId]);
				});
			}
		}
	}
}
