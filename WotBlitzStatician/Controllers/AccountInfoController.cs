namespace WotBlitzStatician.Controllers
{
	using System.Threading.Tasks;
	using Microsoft.AspNetCore.Mvc;
	using WotBlitzStatician.Logic;
	using WotBlitzStatician.ViewModel;

	public class AccountInfoController : Controller
    {
	    private readonly IBlitzStaticianLogic _blitzStaticianLogic;

	    public AccountInfoController(IBlitzStaticianLogic blitzStaticianLogic)
	    {
		    _blitzStaticianLogic = blitzStaticianLogic;
	    }

	    public async Task<IActionResult> Details([FromRoute(Name = "id")]long accountId)
	    {
		    var viewModel = new AccountInfoViewModel();
			viewModel.AccountInfo = await _blitzStaticianLogic.GetAccountStatistics(accountId);

			_blitzStaticianLogic.SetLastLoggedAccount(accountId);

			return View(viewModel);
		}

	}
}
