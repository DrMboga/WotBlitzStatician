namespace WotBlitzStatician.Controllers
{
	using Microsoft.AspNetCore.Mvc;
	using WotBlitzStatician.ViewModel;

	public class AccountInfoController : Controller
    {

		public IActionResult Details([FromRoute(Name = "id")]long accountId)
		{
			var viewModel = new AccountInfoViewModel {AccountId = accountId};
			return View(viewModel);
		}

	}
}
