namespace WotBlitzStatician.WotApiClient
{
	using System;

	internal class ResponseException : Exception
	{
		public ResponseException(string message) : base(message)
		{
		}

		public Error Error { get; set; }
	}
}