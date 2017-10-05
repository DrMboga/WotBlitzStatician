namespace WotBlitzStatician.Logic
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using WotBlitzStatician.Data.DataAccessors;
	using WotBlitzStatician.Model;

	public class BlitzStaticianDictionary : IBlitzStaticianDictionary
	{
		private readonly IStaticInfoDataAccessor _staticInfoDataAccessor;
		private Dictionary<MarkOfMastery, AchievementOption> _marksInfo = null;

		private Dictionary<MarkOfMastery, AchievementOption> MarksInfo
		{
			get
			{
				if (_marksInfo == null)
				{
					FillMarksInfo();
				}
				return _marksInfo;
			}
		}

		public BlitzStaticianDictionary(IStaticInfoDataAccessor staticInfoDataAccessor)
		{
			_staticInfoDataAccessor = staticInfoDataAccessor;
		}

		public string GetMarkOfMasteryImageUrl(MarkOfMastery markOfMastery)
		{
			if (markOfMastery == MarkOfMastery.None)
				return string.Empty;
			return MarksInfo[markOfMastery].Image;
		}

		public string GetMarkOfMasteryBigImageUrl(MarkOfMastery markOfMastery)
		{
			if (markOfMastery == MarkOfMastery.None)
				return string.Empty;
			return MarksInfo[markOfMastery].ImageBig;
		}

		private void FillMarksInfo()
		{
			var achievmentOptions = _staticInfoDataAccessor.GetMarksOfMastery();
			if(achievmentOptions == null || achievmentOptions.Length != 4)
				throw new Exception("Could not read Marks of mastery dictionary");
			_marksInfo = new Dictionary<MarkOfMastery, AchievementOption>();
			// Marks of mastery options are in strong order
			_marksInfo[MarkOfMastery.Rank3] = achievmentOptions[0];
			_marksInfo[MarkOfMastery.Rank2] = achievmentOptions[1];
			_marksInfo[MarkOfMastery.Rank1] = achievmentOptions[2];
			_marksInfo[MarkOfMastery.Master] = achievmentOptions[3];
		}
	}
}