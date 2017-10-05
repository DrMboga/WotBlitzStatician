namespace WotBlitzStatician.Logic.Calculations
{
	using System;
	using AutoMapper;
	using WotBlitzStatician.Logic.Dto;
	using WotBlitzStatician.Model;

	internal static class StatisticsDeltaHelper
	{
		public static void FillStatistics(this BlitzAccountInfoStatisticsDelta delta, AccountInfoStatistics min, AccountInfoStatistics max, decimal intervalTier)
		{
			IMapper mapper = new Mapper(new MapperConfiguration(m => m.CreateMap<AccountInfoStatistics, StatisticsDto>()
										.ForMember(d => d.LastBattle, o => o.MapFrom(s => s.UpdatedAt))
										.ForMember(d => d.Tier, o => o.MapFrom(s => s.AvgTier))));
			delta.Statistics = FillStatistics(
				mapper.Map<AccountInfoStatistics, StatisticsDto>(min),
				mapper.Map<AccountInfoStatistics, StatisticsDto>(max), 
				intervalTier);
		}

		public static void FillStatistics(this BlitzTankInfoDelta delta, AccountTankStatistics min, AccountTankStatistics max)
		{
			IMapper mapper = new Mapper(new MapperConfiguration(m => m.CreateMap<AccountTankStatistics, StatisticsDto>()
				.ForMember(d => d.LastBattle, o => o.MapFrom(s => s.LastBattleTime))
				.ForMember(d => d.Tier, o => o.UseValue((double)delta.Tier))));

			delta.Statistics = FillStatistics(
				mapper.Map<AccountTankStatistics, StatisticsDto>(min),
				mapper.Map<AccountTankStatistics, StatisticsDto>(max),
				delta.Tier);
		}

		private static StatisticsDelta FillStatistics(StatisticsDto statMin, StatisticsDto statMax, decimal intervalTier)
		{
			var statistics = new StatisticsDelta
			{
				Battles = GetLongDelta(statMin.Battles, statMax.Battles),
				LastBattle = GetDateTimeDelta(statMin.LastBattle, statMax.LastBattle),
				Wins = GetLongDelta(statMin.Wins, statMax.Wins),
				Winrate = GetWinrateDelta(statMin.Wins, statMin.Battles, statMax.Wins, statMax.Battles),
				AvgDamage = GetAvgDelta(statMin.DamageDealt, statMin.Battles, statMax.DamageDealt, statMax.Battles),
				AvgXp = GetAvgDelta(statMin.Xp, statMin.Battles, statMax.Xp, statMax.Battles),
				Wn7 = GetWn7Delta(statMin, statMax, intervalTier),
				Effectivity = GetEffectivityDelta(statMin, statMax, intervalTier)
			};

			return statistics;
		}

		private static ValueDelta<long, long> GetLongDelta(long min, long max)
		{
			return new ValueDelta<long, long>(max, min, Math.Abs(max - min), max - min, min > max);
		}

		private static ValueDelta<DateTime, TimeSpan> GetDateTimeDelta(DateTime min, DateTime max)
		{
			return new ValueDelta<DateTime, TimeSpan>(max, min, max - min, max - min);
		}

		private static ValueDelta<decimal, decimal> GetWinrateDelta(long minWins, long minBattles, long maxWins, long maxBattles)
		{
			decimal winrateMax = Convert.ToDecimal(maxWins) * 100 / maxBattles;
			decimal winrateMin = Convert.ToDecimal(minWins) * 100 / minBattles;
			decimal winrateInterval = maxBattles - minBattles == 0 ? 0m : Convert.ToDecimal(maxWins - minWins) * 100 / (maxBattles - minBattles);

			return new ValueDelta<decimal, decimal>(winrateMax, winrateMin, Math.Abs(winrateMax - winrateMin), winrateInterval, winrateMin > winrateMax);
		}

		private static ValueDelta<decimal, decimal> GetAvgDelta(long min, long minBattles, long max, long maxBattles)
		{
			decimal avgMax = Convert.ToDecimal(max) / maxBattles;
			decimal avgMin = Convert.ToDecimal(min) / minBattles;
			decimal avgInterval = maxBattles - minBattles == 0 ? 0m : Convert.ToDecimal(Math.Abs(max - min)) / (maxBattles - minBattles);

			return new ValueDelta<decimal, decimal>(avgMax, avgMin, Math.Abs(avgMax - avgMin), avgInterval, avgMin > avgMax);
		}

		private static ValueDelta<decimal, decimal> GetWn7Delta(StatisticsDto statMin, StatisticsDto statMax, decimal tier)
		{
			double intervalWn7 = 0d;
			if (statMax.Battles > statMin.Battles)
			{
				double intervalFrags = IntervalFrags(statMin, statMax);
				double intervalDamage = IntervalDamage(statMin, statMax);
				double intervalSpot = IntervalSpot(statMin, statMax);
				double intervalDef = IntervalDef(statMin, statMax);
				double intervalRate = IntervalRate(statMin, statMax);

				intervalWn7 = Wn7Helper.CalculateWn7(statMax.Battles - statMin.Battles, (double) tier, intervalFrags,
					intervalDamage, intervalSpot, intervalDef, intervalRate);
			}
			return new ValueDelta<decimal, decimal>((decimal) statMax.Wn7, (decimal) statMin.Wn7,
				(decimal) Math.Abs(statMax.Wn7 - statMin.Wn7), (decimal) intervalWn7, statMin.Wn7 > statMax.Wn7);
		}
		private static ValueDelta<decimal, decimal> GetEffectivityDelta(StatisticsDto statMin, StatisticsDto statMax, decimal tier)
		{
			double intervalEffectivity = 0d;
			if (statMax.Battles > statMin.Battles)
			{
				double intervalFrags = IntervalFrags(statMin, statMax);
				double intervalDamage = IntervalDamage(statMin, statMax);
				double intervalSpot = IntervalSpot(statMin, statMax);
				double intervalCapture = IntervalCapture(statMin, statMax);
				double intervalDef = IntervalDef(statMin, statMax);

				intervalEffectivity = EffectivityHelper.Effectivity(intervalDamage, (double) tier, intervalFrags, intervalSpot,
					intervalCapture, intervalDef);
			}
			return new ValueDelta<decimal, decimal>((decimal) statMax.Effectivity, (decimal) statMin.Effectivity,
				(decimal) Math.Abs(statMax.Effectivity - statMin.Effectivity), (decimal) intervalEffectivity,
				statMin.Effectivity > statMax.Effectivity);
		}

		private static double IntervalRate(StatisticsDto statMin, StatisticsDto statMax)
		{
			return (100 * (double)(statMax.Wins - statMin.Wins)/ (statMax.Battles - statMin.Battles));
		}

		private static double IntervalDef(StatisticsDto statMin, StatisticsDto statMax)
		{
			return (double) (statMax.DroppedCapturePoints - statMin.DroppedCapturePoints) / (statMax.Battles - statMin.Battles);
		}
		private static double IntervalCapture(StatisticsDto statMin, StatisticsDto statMax)
		{
			return (double)(statMax.CapturePoints - statMin.CapturePoints)/ (statMax.Battles - statMin.Battles);
		}

		private static double IntervalSpot(StatisticsDto statMin, StatisticsDto statMax)
		{
			return (double)(statMax.Spotted - statMin.Spotted) / (statMax.Battles - statMin.Battles);
		}

		private static double IntervalDamage(StatisticsDto statMin, StatisticsDto statMax)
		{
			return (double)(statMax.DamageDealt - statMin.DamageDealt) / (statMax.Battles - statMin.Battles);
		}

		private static double IntervalFrags(StatisticsDto statMin, StatisticsDto statMax)
		{
			return (double)(statMax.Frags - statMin.Frags) / (statMax.Battles - statMin.Battles);
		}

	}
}