namespace WotBlitzStatician.Logic
{
    using WotBlitzStatician.Logic.Dto;

    public interface IBlitzStatitianDataAnalyser
    {
        BlitzAccountInfoStatisticsDelta GetAccountLastSessionDelta(long accountId);
    }
}
