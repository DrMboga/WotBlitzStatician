namespace WotBlitzStatician.ViewComponents
{
	using System.Threading.Tasks;
	using Microsoft.AspNetCore.Mvc;
	using WotBlitzStatician.Logic.Dto;
	using WotBlitzStatician.Model.MapperLogic;
	using WotBlitzStatician.ViewModel;

	public class TankDelta : ViewComponent
	{
		private readonly IMapperHelper _mapper;

		public TankDelta(IMapperHelper mapper)
		{
			_mapper = mapper;
		}

		public Task<IViewComponentResult> InvokeAsync(BlitzTankInfoDelta tankDelta)
		{
			if (tankDelta == null)
			{
				return Task.FromResult<IViewComponentResult>(View("Unknown", 0));
			}
			var viewModel = _mapper.Map<BlitzTankInfoDelta, TankDeltaViewModel> (tankDelta);

			return Task.FromResult<IViewComponentResult>(View(viewModel));
		}

	}
}