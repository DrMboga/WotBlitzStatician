namespace WotBlitzStatician.ViewModel
{
	using System.Collections.Generic;
	using WotBlitzStatician.Model;

	public class SearchPlayerViewModel
	{
		public string SearchingNick { get; set; }

		public List<AccountInfo> Accounts { get; set; }

	}
}