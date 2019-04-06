using WotBlitzStatician.Model.Dto;

namespace WotBlitzStatician.Logic
{
  public interface IStatisticsCollectorFactory
  {
    IStatisticsCollector CreateCollector(long? accountId = null);
    IStatisticsCollector CreateCollector(long accountId, GuestAccountInfo guestAccountInfo);
  }
}