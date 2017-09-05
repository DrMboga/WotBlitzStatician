namespace WotBlitzStatician.Controllers
{
	using System.Threading.Tasks;
	using Microsoft.AspNetCore.Mvc;
	using WotBlitzStatician.Logic;
    using WotBlitzStatician.Mappers;
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
			var accountInfo = await _blitzStaticianLogic.GetAccountStatistics(accountId);
            var accountInfoMapper = new AccountInfoMapper();

            var viewModel = accountInfoMapper.Map(accountInfo);

			_blitzStaticianLogic.SetLastLoggedAccount(accountId);

			// toDo: Create mapper
		    viewModel.Wn7Style = viewModel.Wn7Grade.GetWn7GradationStyle();
		    viewModel.Wn7GradeS = viewModel.Wn7Grade.ToString();


			return View(viewModel);
		}

	}
}
