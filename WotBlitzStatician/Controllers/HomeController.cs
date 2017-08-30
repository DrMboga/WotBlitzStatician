namespace WotBlitzStatician.Controllers
{
	using System.Threading.Tasks;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.Routing;
	using WotBlitzStatician.Logic;
    using WotBlitzStatician.ViewModel;

	public class HomeController : Controller
	{
		private readonly IBlitzStaticianLogic _blitzStaticianLogic;

		public HomeController(IBlitzStaticianLogic blitzStaticianLogic)
		{
			_blitzStaticianLogic = blitzStaticianLogic;
		}

        public IActionResult Index()
        {
	        var lastAccount = _blitzStaticianLogic.GetLastLoggedAccount();
	        if (lastAccount != null)
	        {
				return RedirectToAction("Details", "AccountInfo", new RouteValueDictionary { { "id", lastAccount.AccountId } });
			}
	        return SearchPlayer();
        }

		public IActionResult SearchPlayer()
		{
			return View("SearchPlayer");
		}

		[HttpPost]
		public async Task<IActionResult> SearchPlayer(SearchPlayerViewModel model)
		{
			model.Accounts = await _blitzStaticianLogic.FindAccounts(model.SearchingNick);

			return View(model);
		}
	}
}
