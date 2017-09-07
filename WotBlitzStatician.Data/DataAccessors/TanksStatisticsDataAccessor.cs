namespace WotBlitzStatician.Data.DataAccessors
{
	using System.Collections.Generic;
	using System.Linq;
	using log4net;
	using Microsoft.EntityFrameworkCore;
	using WotBlitzStatician.Model;

	public class TanksStatisticsDataAccessor : ITanksStatisticsDataAccessor
	{
		private static readonly ILog _log = LogManager.GetLogger(typeof(TanksStatisticsDataAccessor));
		private readonly IBlitzStaticianDataContextFactory _blitzStaticianDataContextFactory;

		public TanksStatisticsDataAccessor(IBlitzStaticianDataContextFactory blitzStaticianDataContextFactory)
		{
			_blitzStaticianDataContextFactory = blitzStaticianDataContextFactory;
		}

		public AccountTankStatistics GetAllTanksByAccount(long accountId)
		{
			throw new System.NotImplementedException();
		}

		public void SaveTanksStatistic(List<AccountTankStatistics> taksStat)
		{
			using (var context = _blitzStaticianDataContextFactory.CreateContext())
			{
				context.AccountTankStatistics.AddRange(taksStat);
				context.SaveChanges();
			}
			_log.Debug($"<SaveTanksStatistic> saved {taksStat.Count()} tanks");
		}

		public void SaveAccountTankAchievements(List<AccountInfoTankAchievment> accountInfoTankAchievments)
		{
			if (accountInfoTankAchievments == null || !accountInfoTankAchievments.Any())
				return;
			using (var context = _blitzStaticianDataContextFactory.CreateContext())
			{
				foreach (var accountInfoTankAchievment in accountInfoTankAchievments)
				{
					long accountInfoTankAchievmentId = context.AccountInfoTankAchievment
						.Where(a => a.AccountId == accountInfoTankAchievment.AccountId && a.AchievementId == accountInfoTankAchievment.AchievementId && a.IsMaxSeries == accountInfoTankAchievment.IsMaxSeries && a.TankId == accountInfoTankAchievment.TankId)
						.Select(a => a.AccountInfoAchievementId)
						.FirstOrDefault();
					if (default(long) == accountInfoTankAchievmentId)
					{
						context.AccountInfoTankAchievment.Add(accountInfoTankAchievment);
					}
					else
					{
						accountInfoTankAchievment.AccountInfoAchievementId = accountInfoTankAchievmentId;
						context.AccountInfoTankAchievment.Attach(accountInfoTankAchievment);
						context.Entry(accountInfoTankAchievment).State = EntityState.Modified;
					}

				}
				context.SaveChanges();
			}
			_log.Debug($"<SaveAccountTankAchievements> saved {accountInfoTankAchievments.Count()} tank acievements.");
		}
	}
}