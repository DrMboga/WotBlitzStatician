using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using WotBlitzStatician.Data.DataAccessors;

namespace WotBlitzStatician.Controllers
{
  public class TanksInfoController : ODataController
  {
    private readonly IAccountsTankInfoDataAccessor _dataAccessor;

    public TanksInfoController(IAccountsTankInfoDataAccessor dataAccessor)
    {
      _dataAccessor = dataAccessor;
    }

    // /api/TanksInfo(90277267)?$filter=VehicleTier eq 6 and TankInGarage eq true
    [EnableQuery]
    [ODataRoute("TanksInfo({accountId})")]
    public IActionResult Get(long accountId)
    {
      return Ok(_dataAccessor.GetTanksInfoQuery(accountId));
    }
  }
}
