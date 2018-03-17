using System.Collections.Generic;
using WotBlitzStatician.Model;

namespace WotBlitzStatician.Logic.StatisticsCollectorOperations.OperationContext
{
	public class SatisticsCollectorAccountOperationContext
    {
		public AccountInfo CurrentAccountInfo { get; set; }
		public AccountInfo WargamingAccountInfo { get; set; }
		public List<AccountTankStatistics> AccountInfoTanks { get; set; }
	}
}
