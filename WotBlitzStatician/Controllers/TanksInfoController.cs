using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using WotBlitzStatician.Data.DataAccessors;

namespace WotBlitzStatician.Controllers
{
  public class TanksInfoController : ODataController
  {
    private readonly IAccountsTankInfoDataAccessor _dataAccessor;
    private GuestAccountCache _guestAccountCache;

    public TanksInfoController(IAccountsTankInfoDataAccessor dataAccessor,
                              GuestAccountCache guestAccountCache)
    {
      _dataAccessor = dataAccessor;
      _guestAccountCache = guestAccountCache;
    }

    // /api/TanksInfo(90277267)?$filter=VehicleTier eq 6 and TankInGarage eq true
    [EnableQuery]
    [ODataRoute("TanksInfo({accountId})")]
    public IActionResult Get(long accountId)
    {
      return Ok(_dataAccessor.GetTanksInfoQuery(accountId));
    }

    // /api/GuestTanksInfo(90277267)?$filter=VehicleTier eq 6
    [EnableQuery]
    [ODataRoute("GuestTanksInfo({accountId})")]
    public async Task<IActionResult> GetGuestTanks(long accountId)
    {
      var accountInfo = await _guestAccountCache.ReadAccountInfoFromCache(accountId);

      return Ok(accountInfo.Tanks.AsQueryable());
    }
  }
}
