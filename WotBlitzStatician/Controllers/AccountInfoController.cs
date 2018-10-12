using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IAccountsTankInfoDataAccessor _accountsTankInfoDataAccessor;

        public AccountInfoController(
          IAccountDataAccessor accountDataAccessor,
          IClanInfoDataAccessor clanInfoDataAccessor,
          IAchievementsDataAccessor achievementsDataAccessor,
          IAccountsTankInfoDataAccessor accountsTankInfoDataAccessor)
        {
            _accountDataAccessor = accountDataAccessor;
            _clanInfoDataAccessor = clanInfoDataAccessor;
            _achievementsDataAccessor = achievementsDataAccessor;
            _accountsTankInfoDataAccessor = accountsTankInfoDataAccessor;
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

            var tanksInfos = await _accountsTankInfoDataAccessor.GetStringTankInfos(new long[] {
                                account.PlayerStatistics.MaxFragsTankId,
                                account.PlayerStatistics.MaxXpTankId });

            if (tanksInfos.Exists(t => t.tankId == account.PlayerStatistics.MaxFragsTankId))
            {
                account.PlayerStatistics.MaxFragsTankInfo = tanksInfos
                .First(t => t.tankId == account.PlayerStatistics.MaxFragsTankId)
                .tankInfo;
            }

            if (tanksInfos.Exists(t => t.tankId == account.PlayerStatistics.MaxXpTankId))
            {
                account.PlayerStatistics.MaxXpTankInfo = tanksInfos
                .First(t => t.tankId == account.PlayerStatistics.MaxXpTankId)
                .tankInfo;
            }

            account.AccountMasteryInfo = await _accountsTankInfoDataAccessor.GetAccountMasteryInfo(id);

            return Ok(account);
        }

        // api/AccountInfo/AccountStatHistory/90277267?dateFrom=2018-08-07
        [HttpGet("AccountStatHistory/{accountId}")]
        public async Task<IActionResult> GetAccountStatHistory(long accountId, [FromQuery] DateTime dateFrom)
        {
            var playerHistory = await _accountDataAccessor.GetAccountStatHistory(accountId, dateFrom);
            return Ok(playerHistory);
        }

        [HttpGet("ShortAccountInfo/{accountId}")]
        public async Task<IActionResult> GetShortAccountInfo(long accountId)
        {
            var shortAccount = await _accountDataAccessor.GetShortAccountInfo(accountId);
            if (shortAccount == null)
            {
                return NotFound();
            }
            return Ok(shortAccount);
        }

        [HttpPut("{accountId}")]
        public async Task<IActionResult> UpdateAccountInfo(long accountId, [FromBody]AccountInfo accountInfo)
        {
            var accountInfoFromDb = await _accountDataAccessor.GetShortAccountInfo(accountId);
            if (accountInfoFromDb == null)
            {
                await _accountDataAccessor.InsertAccountAuthInfoAsync(accountInfo);
            }
            else
            {
                await _accountDataAccessor.SaveProlongedAccountAsync(accountInfo.AccountId, accountInfo.AccessToken, accountInfo.AccessTokenExpiration.Value);
            }
            return NoContent();
        }
    }
}
