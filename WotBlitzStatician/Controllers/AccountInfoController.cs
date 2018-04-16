using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WotBlitzStatician.Model;

namespace WotBlitzStatician.Controllers
{
	[Route("api/[controller]")]
    public class AccountInfoController : Controller
    {
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<AccountInfo> Get()
        {
            return new [] 
			{
				new AccountInfo {AccountId = 20, NickName="Account 20", LastBattleTime = DateTime.Now.AddHours(-3)},
				new AccountInfo {AccountId = 25, NickName="Account 25", LastBattleTime = DateTime.Now.AddHours(-2)}
			};
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
