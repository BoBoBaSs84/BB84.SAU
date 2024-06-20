namespace BB84.SAU.Application.Common;

/// <summary>
/// The application constants class.
/// </summary>
internal static class ApplicationConstants
{
	/// <summary>
	/// The http clients class.
	/// </summary>
	internal static class HttpClients
	{
		/// <summary>
		/// The steam store http client constants.
		/// </summary>
		internal static class SteamStore
		{
			/// <summary>
			/// The base uri for api calls.
			/// </summary>
			internal const string BaseUri = "https://store.steampowered.com";

			/// <summary>
			/// Returns the details about an steam application.
			/// </summary>
			internal const string GetAppDetails = "api/appdetails?appids={0}";
		}

		/// <summary>
		/// The steam powered http client constants.
		/// </summary>
		internal static class SteamPowered
		{
			/// <summary>
			/// The base uri for api calls.
			/// </summary>
			internal const string BaseUri = "https://api.steampowered.com";

			/// <summary>
			/// Returns basic profile information.
			/// </summary>
			internal const string GetPlayerSummaries = "ISteamUser/GetPlayerSummaries/v2/?key={0}&steamids={1}";

			/// <summary>
			/// Returns a list of games a player owns along with some playtime information, if the profile is publicly visible.
			/// </summary>
			internal const string GetOwnedGames = "IPlayerService/GetOwnedGames/v1/?key={0}&steamid={1}&include_appinfo=true&include_played_free_games=true";

			/// <summary>
			/// Returns the complete list of stats and achievements for the specified game.
			/// </summary>
			internal const string GetSchemaForGame = "ISteamUserStats/GetSchemaForGame/v2/?key={0}&appid={1}";
		}
	}
}
