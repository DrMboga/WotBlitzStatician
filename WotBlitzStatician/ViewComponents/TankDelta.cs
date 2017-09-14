namespace WotBlitzStatician.ViewComponents
{
	using System;
	using System.Threading.Tasks;
	using Microsoft.AspNetCore.Mvc;
	using WotBlitzStatician.Logic;
	using WotBlitzStatician.Mappers;

	public class TankDelta : ViewComponent
	{
		private readonly IBlitzStatitianDataAnalyser _dataAnalyser;

		public TankDelta(IBlitzStatitianDataAnalyser dataAnalyser)
		{
			_dataAnalyser = dataAnalyser;
		}

		public Task<IViewComponentResult> InvokeAsync(long tankId, long accountId, DateTime dateFrom)
		{
			var mapper = new TankDeltaMapper();
			var tankDelta = _dataAnalyser.GeTankInfoDelta(accountId, tankId, dateFrom);
			if (tankDelta == null)
			{
				return Task.FromResult<IViewComponentResult>(View("Unknown", tankId));
			}
			var viewModel = mapper.Map(tankDelta);

			return Task.FromResult<IViewComponentResult>(View(viewModel));
		}

	}
}