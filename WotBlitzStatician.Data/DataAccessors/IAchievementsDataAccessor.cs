using System.Collections.Generic;
using System.Threading.Tasks;
using WotBlitzStatician.Model.Dto;

namespace WotBlitzStatician.Data.DataAccessors
{
	public interface IAchievementsDataAccessor
    {
		Task<List<AchievementDto>> GetAccountAchievements(long accountId);
    }
}
