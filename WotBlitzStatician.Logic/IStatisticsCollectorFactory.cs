namespace WotBlitzStatician.Logic
{
    public interface IStatisticsCollectorFactory
    {
        IStatisticsCollector CreateCollector(long? accountId = null);
    }
}