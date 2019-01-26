using System.Threading.Tasks;
using WotBlitzStatician.Model;
using WotBlitzStatician.Model.Dto;

namespace WotBlitzStatician.Data.DataAccessors
{
    public interface IClanInfoDataAccessor
    {
        Task<PlayerClanInfoDto> GetClanInfo(long accountId);

        Task<AccountClanInfo> GetAccountClanAsync(long accountId);

    }
}
