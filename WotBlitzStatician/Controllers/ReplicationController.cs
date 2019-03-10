using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WotBlitzStatician.Data.DataAccessors;
using WotBlitzStatician.Model;

namespace WotBlitzStatician.Controllers
{
  [Authorize]
  [Route("api/[controller]")]

  public class ReplicationController : Controller
  {
    private readonly IReplicationDataAccessor _replicationDataAccessor;

    public ReplicationController(IReplicationDataAccessor replicationDataAccessor)
    {
      _replicationDataAccessor = replicationDataAccessor;
    }

    [HttpGet()]
    public IActionResult ExportDatabase()
    {
      var allDatabase = _replicationDataAccessor.GetDatabase();
      return Ok(allDatabase);
    }

    [HttpPost()]
    public IActionResult ImportDatabase([FromBody] ReplicationData replicationData)
    {
      _replicationDataAccessor.SetDatabase(replicationData);
      return Ok();
    }

  }
}
