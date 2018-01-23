using System.Collections.Generic;
using Newtonsoft.Json;

namespace WotBlitzStatician.WotApiClient.InternalModel
{
    internal class WotClanGlossaryResponse
    {
		[JsonProperty("clans_roles")]
		public Dictionary<string, string> ClanRoles { get; set; }

		[JsonProperty("settings")]
		public Dictionary<string, string> Settings { get; set; }
	}
}
