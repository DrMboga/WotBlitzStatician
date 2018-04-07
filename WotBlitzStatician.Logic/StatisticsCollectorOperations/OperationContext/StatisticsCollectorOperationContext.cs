using System.Collections.Generic;

namespace WotBlitzStatician.Logic.StatisticsCollectorOperations.OperationContext
{
	public class StatisticsCollectorOperationContext
    {
		public OperationState OperationState { get; set; } = OperationState.Ok;
		public string OperationStateMessage { get; set; }

		public List<SatisticsCollectorAccountOperationContext> Accounts { get; set; }
			= new List<SatisticsCollectorAccountOperationContext>();
	}
}
