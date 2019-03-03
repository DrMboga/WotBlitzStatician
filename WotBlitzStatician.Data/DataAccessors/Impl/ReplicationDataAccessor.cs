using System.Linq;
using Microsoft.EntityFrameworkCore;
using WotBlitzStatician.Model;

namespace WotBlitzStatician.Data.DataAccessors.Impl
{
  public class ReplicationDataAccessor : IReplicationDataAccessor
  {
    private readonly BlitzStaticianDbContext _dbContext;

    public ReplicationDataAccessor(BlitzStaticianDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public ReplicationData GetDatabase()
    {
      var replicationData = new ReplicationData();
      replicationData.AccountInfo = _dbContext.AccountInfo.AsNoTracking().ToList();
      replicationData.AccountInfo.ForEach(i =>
      {
        i.AccessToken = string.Empty;
        i.AccessTokenExpiration = null;
      });

      replicationData.AccountInfoStatistics = _dbContext.AccountInfoStatistics.AsNoTracking().ToList();

      return replicationData;
    }

    public void SetDatabase(ReplicationData replicationData)
    {
      throw new System.NotImplementedException();
    }
  }
}