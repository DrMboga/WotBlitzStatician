using System.Threading.Tasks;
using WotBlitzStatician.Model.Dto;

namespace WotBlitzStatician.Data.DataAccessors
{
	public interface IClanInfoDataAccessor
    {
		Task<PlayerClanInfoDto> GetClanInfo(long accountId);
    }
}
