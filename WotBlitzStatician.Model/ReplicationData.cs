using System.Collections.Generic;

namespace WotBlitzStatician.Model
{
    public class ReplicationData
    {
        public List<AccountInfo> AccountInfo { get; set; }
        public List<AccountInfoStatistics> AccountInfoStatistics { get; set; }
    }
}