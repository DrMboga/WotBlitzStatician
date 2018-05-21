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

		public AccountInfoController(IAccountDataAccessor accountDataAccessor)
		{
			_accountDataAccessor = accountDataAccessor;
		}

		// GET: api/<controller>
		[HttpGet]
        public async Task<IEnumerable<AccountInfo>> Get()
        {
			return await _accountDataAccessor.GetAllAccountsAsync();
		}

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public AccountInfo Get(int id)
        {
			return
				new AccountInfo { AccountId = id, NickName = $"Account {id}", LastBattleTime = DateTime.Now.AddHours(-2) };
		}
    }
}
