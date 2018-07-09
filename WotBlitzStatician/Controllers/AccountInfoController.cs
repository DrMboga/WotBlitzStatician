using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WotBlitzStatician.Data.DataAccessors;
using WotBlitzStatician.Model;

namespace WotBlitzStatician.Controllers
{
  [Route("api/[controller]")]
  public class AccountInfoController : Controller
  {
    private readonly IAccountDataAccessor _accountDataAccessor;
    private readonly IClanInfoDataAccessor _clanInfoDataAccessor;
    private readonly IAchievementsDataAccessor _achievementsDataAccessor;

    public AccountInfoController(
      IAccountDataAccessor accountDataAccessor,
      IClanInfoDataAccessor clanInfoDataAccessor,
      IAchievementsDataAccessor achievementsDataAccessor)
    {
      _accountDataAccessor = accountDataAccessor;
      _clanInfoDataAccessor = clanInfoDataAccessor;
      _achievementsDataAccessor = achievementsDataAccessor;
    }

    // GET: api/<controller>
    [HttpGet]
    public async Task<IEnumerable<AccountInfo>> Get()
    {
      return await _accountDataAccessor.GetAllAccountsAsync();
    }

    // GET api/<controller>/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
      var account = await _accountDataAccessor.GetActualAccountInfo(id);

      if (account == null)
      {
        return NotFound();
      }

      account.PlayerClanInfo = await _clanInfoDataAccessor.GetClanInfo(id);
      account.Achievements = await _achievementsDataAccessor.GetAccountAchievements(id);

      return Ok(account);
    }
  }
}
