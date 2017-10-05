namespace WotBlitzStatician.Controllers
{
	using System.Linq;
	using System.Threading.Tasks;
	using Microsoft.AspNetCore.Mvc;
	using WotBlitzStatician.Logic;
	using WotBlitzStatician.Logic.Dto;
	using WotBlitzStatician.Model;
	using WotBlitzStatician.Model.MapperLogic;
	using WotBlitzStatician.ViewModel;

	public class AccountInfoController : Controller
    {
	    private readonly IBlitzStaticianLogic _blitzStaticianLogic;
        private readonly IBlitzStatitianDataAnalyser _dataAnalyser;
	    private readonly IMapperHelper _mapper;

		public AccountInfoController(
            IBlitzStaticianLogic blitzStaticianLogic,
            IBlitzStatitianDataAnalyser dataAnalyser, IMapperHelper mapper)
	    {
		    _blitzStaticianLogic = blitzStaticianLogic;
            _dataAnalyser = dataAnalyser;
		    _mapper = mapper;
	    }

	    public async Task<IActionResult> Details([FromRoute(Name = "id")]long accountId)
	    {
			var accountInfo = await _blitzStaticianLogic.GetAccountStatistics(accountId);

		    var viewModel = _mapper.Map<AccountInfo, AccountInfoViewModel>(accountInfo);

			_blitzStaticianLogic.SetLastLoggedAccount(accountId);

		    viewModel.PreLastUpdatedDate = _dataAnalyser.GetPrelastStatisticsUpdateDate(accountId);

            var delta = _dataAnalyser.GetAccountDelta(accountId, viewModel.PreLastUpdatedDate);
		    viewModel.AccountInfoDelta = _mapper.Map<BlitzAccountInfoStatisticsDelta, AccountInfoDeltaViewModel>(delta);
		    viewModel.TanksDelta = delta.TanksForPeriod.OrderByDescending(t => t.Statistics.LastBattle.PresentValue).ToList();

			return View(viewModel);
		}

	    public async Task<IActionResult> DetailsWithDate(AccountInfoViewModel model)
	    {
		    var accountInfo = await _blitzStaticianLogic.GetAccountStatistics(model.AccountId);

		    var viewModel = _mapper.Map<AccountInfo, AccountInfoViewModel>(accountInfo);
		    viewModel.PreLastUpdatedDate = model.PreLastUpdatedDate;
		    var delta = _dataAnalyser.GetAccountDelta(model.AccountId, viewModel.PreLastUpdatedDate);
		    viewModel.AccountInfoDelta = _mapper.Map<BlitzAccountInfoStatisticsDelta, AccountInfoDeltaViewModel>(delta);
		    viewModel.TanksDelta = delta.TanksForPeriod.OrderByDescending(t => t.Statistics.LastBattle.PresentValue).ToList();

		    return View("Details", viewModel);
		}

	}
}
