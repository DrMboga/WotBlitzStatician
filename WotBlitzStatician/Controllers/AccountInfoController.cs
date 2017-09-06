namespace WotBlitzStatician.Controllers
{
	using System.Threading.Tasks;
	using Microsoft.AspNetCore.Mvc;
	using WotBlitzStatician.Logic;
    using WotBlitzStatician.Mappers;

	public class AccountInfoController : Controller
    {
	    private readonly IBlitzStaticianLogic _blitzStaticianLogic;

	    public AccountInfoController(IBlitzStaticianLogic blitzStaticianLogic)
	    {
		    _blitzStaticianLogic = blitzStaticianLogic;
	    }

	    public async Task<IActionResult> Details([FromRoute(Name = "id")]long accountId)
	    {
			var accountInfo = await _blitzStaticianLogic.GetAccountStatistics(accountId);
            var accountInfoMapper = new AccountInfoMapper();

            var viewModel = accountInfoMapper.Map(accountInfo);

			_blitzStaticianLogic.SetLastLoggedAccount(accountId);

			return View(viewModel);
		}

	}
}
