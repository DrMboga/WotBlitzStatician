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

			// toDo: Create mapper
		    var wn7Grade = viewModel.AccountInfo.AccountInfoStatistics.Wn7.GetWn7();
		    viewModel.Wn7Style = wn7Grade.GetWn7GradationStyle();
		    viewModel.Wn7Grade = wn7Grade.ToString();

			return View(viewModel);
		}

	}
}
