using System.Collections.Generic;
using WotBlitzStatician.Model;

namespace WotBlitzStatician.Logic.StatisticsCollectorOperations
{
	public class StatisticsCollectorOperationContext
    {
		public OperationState OperationState { get; set; }
		public string OperationStateMessage { get; set; }

		public List<AccountInfo> Accounts { get; set; }
	}
}
