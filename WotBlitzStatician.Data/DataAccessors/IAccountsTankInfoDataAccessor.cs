using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WotBlitzStatician.Model;
using WotBlitzStatician.Model.Dto;

namespace WotBlitzStatician.Data.DataAccessors
{
	public interface IAccountsTankInfoDataAccessor
    {
		Task<List<(long tankId, string tankInfo)>> GetStringTankInfos(long[] tankIds);
		Task<List<AccountMasteryInfoDto>> GetAccountMasteryInfo(long accountId);
		IQueryable<AccountTankInfoDto> GetTanksInfoQuery(long accountId);

		Task<AccountTankByAchievementDto[]> GetAllTanksByAchievement(long accountId, string achievementId);
	}
}
