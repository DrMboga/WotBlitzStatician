namespace WotBlitzStatician.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using WotBlitzStatician.Logic;
    using WotBlitzStatician.ViewModel;

    public class AllTanksController : Controller
    {
        private readonly IBlitzStatitianDataAnalyser _dataAnalyser;

        public AllTanksController(IBlitzStatitianDataAnalyser dataAnalyser)
        {
            _dataAnalyser = dataAnalyser;
        }

        public async Task<IActionResult> Details(long accountId)
        {
            var viewModel = new AllTanksViewModel {AccountId = accountId};
            return View(viewModel);
        }
    }
}
