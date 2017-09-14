namespace WotBlitzStatician.Logic
{
	using WotBlitzStatician.Model;

	public interface IBlitzStaticianDictionary
	{
		string GetMarkOfMasteryImageUrl(MarkOfMastery markOfMastery);
		string GetMarkOfMasteryBigImageUrl(MarkOfMastery markOfMastery);
	}
}