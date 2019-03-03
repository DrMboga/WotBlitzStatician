using WotBlitzStatician.Model;

namespace WotBlitzStatician.Data.DataAccessors
{
  public interface IReplicationDataAccessor
  {
    ReplicationData GetDatabase();

    void SetDatabase(ReplicationData replicationData);
  }
}