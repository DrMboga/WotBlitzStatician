using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WotBlitzStatician.Model.Dto;

namespace WotBlitzStatician.Data.DataAccessors
{
    public interface IAccountInfoViewDataAccessor
    {
        Task<AccountInfoDto> GetActualAccountInfo(long accountId);
        Task<List<PlayerStatHistoryDto>> GetAccountStatHistory(long accountId, DateTime dateFrom);

    }
}