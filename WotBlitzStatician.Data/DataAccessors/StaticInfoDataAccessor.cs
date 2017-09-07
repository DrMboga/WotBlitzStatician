namespace WotBlitzStatician.Data.DataAccessors
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using log4net;
	using WotBlitzStatician.Model;

	public class StaticInfoDataAccessor : IStaticInfoDataAccessor
	{
		private static DateTime DefaultDate => new DateTime(1980, 3, 28);
		private static readonly ILog _log = LogManager.GetLogger(typeof(StaticInfoDataAccessor));
		private readonly IBlitzStaticianDataContextFactory _blitzStaticianDataContextFactory;

		public StaticInfoDataAccessor(IBlitzStaticianDataContextFactory blitzStaticianDataContextFactory)
		{
			_blitzStaticianDataContextFactory = blitzStaticianDataContextFactory;
		}


		public DateTime GetStaticDataLastUpdateDate()
		{
			using (var context = _blitzStaticianDataContextFactory.CreateContext())
			{
				if (!context.DictionaryLanguage.Any())
					return DefaultDate;
				return context.DictionaryLanguage.First().LastUpdated;
			}
		}

		public Dictionary<long, double> GetVehicleTires()
		{
			using (var context = _blitzStaticianDataContextFactory.CreateContext())
			{
				return context.VehicleEncyclopedia.Select(v => new { v.TankId, v.Tier }).ToDictionary(k => k.TankId, v => (double)v.Tier);
			}
		}

		public void SaveLanguagesDictionary(List<DictionaryLanguage> languages)
		{
			using (var context = _blitzStaticianDataContextFactory.CreateContext())
			{
				context.Merge(context.DictionaryLanguage, languages);
				context.SaveChanges();
			}
		}

		public void SaveNationsDictionary(List<DictionaryNations> nations)
		{
			using (var context = _blitzStaticianDataContextFactory.CreateContext())
			{
				context.Merge(context.DictionaryNations, nations);
				context.SaveChanges();
			}
		}

		public void SaveVehicleTypesDictionary(List<DictionaryVehicleType> vehicleTypes)
		{
			using (var context = _blitzStaticianDataContextFactory.CreateContext())
			{
				context.Merge(context.DictionaryVehicleType, vehicleTypes);
				context.SaveChanges();
			}
		}

		public void SaveVehicleEncyclopedia(List<VehicleEncyclopedia> vehicles)
		{
			using (var context = _blitzStaticianDataContextFactory.CreateContext())
			{
				context.Merge(context.VehicleEncyclopedia, vehicles);
				context.SaveChanges();
			}
		}

		public void SaveAchievementsDictionary(List<Achievement> achievements)
		{
			var achievementOptions = new List<AchievementOption>();
			achievements
				.Where(a => a.Options != null).ToList()
				.ForEach(a => achievementOptions.AddRange(a.Options));

			using (var context = _blitzStaticianDataContextFactory.CreateContext())
			{
				context.Merge(context.Achievement, achievements);
				context.Merge(context.AchievementOption, achievementOptions);
				context.SaveChanges();
			}
		}
	}
}