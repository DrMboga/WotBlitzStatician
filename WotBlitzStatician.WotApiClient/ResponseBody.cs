namespace WotBlitzStatician.WotApiClient
{
	using Newtonsoft.Json;
	internal class ResponseBody<T>
	{
		[JsonProperty("status")]
		public string Status { get; set; }

		[JsonProperty("meta")]
		public Meta Meta { get; set; }

		[JsonProperty("data")]
		public T Data { get; set; }

		[JsonProperty("error")]
		public Error Error { get; set; }
	}

	internal class Meta
	{
		[JsonProperty("count")]
		public int Count { get; set; }
	}

	internal class Error
	{
		[JsonProperty("field")]
		public string Field { get; set; }

		[JsonProperty("message")]
		public string Message { get; set; }

		[JsonProperty("code")]
		public string Code { get; set; }

		[JsonProperty("value")]
		public string Value { get; set; }

	}
}