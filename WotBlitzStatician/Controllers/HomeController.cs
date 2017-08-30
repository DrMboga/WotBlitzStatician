namespace WotBlitzStatician.Controllers
{
	using System.Threading.Tasks;
	using Microsoft.AspNetCore.Mvc;
    using WotBlitzStatician.Logic;
    using WotBlitzStatician.ViewModel;

	public class HomeController : Controller
	{
		private readonly IBlitzStaticianLogic _blitzStaticianLogic;

		public HomeController(IBlitzStaticianLogic blitzStaticianLogic)
		{
			_blitzStaticianLogic = blitzStaticianLogic;
		}

		// GET: /<controller>/
        public IActionResult Index()
        {
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

		public string Details([FromRoute(Name = "id")]long accountId)
		{
			return $"Details for accountId {accountId}";
		}


	}
}
