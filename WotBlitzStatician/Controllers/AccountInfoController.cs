namespace WotBlitzStatician.Controllers
{
	using System.Linq;
	using System.Threading.Tasks;
	using Microsoft.AspNetCore.Mvc;
	using WotBlitzStatician.Logic;
    using WotBlitzStatician.Mappers;

	public class AccountInfoController : Controller
    {
	    private readonly IBlitzStaticianLogic _blitzStaticianLogic;
        private readonly IBlitzStatitianDataAnalyser _dataAnalyser;

	    public AccountInfoController(
            IBlitzStaticianLogic blitzStaticianLogic,
            IBlitzStatitianDataAnalyser dataAnalyser)
	    {
		    _blitzStaticianLogic = blitzStaticianLogic;
            _dataAnalyser = dataAnalyser;
	    }

	    public async Task<IActionResult> Details([FromRoute(Name = "id")]long accountId)
	    {
			var accountInfo = await _blitzStaticianLogic.GetAccountStatistics(accountId);
            var accountInfoMapper = new AccountInfoMapper();

            var viewModel = accountInfoMapper.Map(accountInfo);

			_blitzStaticianLogic.SetLastLoggedAccount(accountId);

		    viewModel.PreLastUpdatedDate = _dataAnalyser.GetPrelastStatisticsUpdateDate(accountId);

            var delta = _dataAnalyser.GetAccountDelta(accountId, viewModel.PreLastUpdatedDate);
		    var deltaMapper = new AccountInfoDeltaMapper();
		    viewModel.AccountInfoDelta = deltaMapper.Map(delta);

		    viewModel.LastSessionTanks = _dataAnalyser.GetTankIdsByLastSession(accountId, viewModel.PreLastUpdatedDate);

			return View(viewModel);
		}

	}
}
