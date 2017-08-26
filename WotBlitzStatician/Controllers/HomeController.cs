namespace WotBlitzStatician.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : Controller
    {
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
		public IActionResult SearchPlayer(string PlayerName)
		{
			// ToDo: Create viewModel,
			// https://docs.microsoft.com/ru-ru/aspnet/core/tutorials/first-mvc-app/search
			return View();
		}

	}
}
