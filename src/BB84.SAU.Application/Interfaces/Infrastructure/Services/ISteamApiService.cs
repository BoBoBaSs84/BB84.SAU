namespace BB84.SAU.Application.Interfaces.Infrastructure.Services;

/// <summary>
/// The steam sdk api service interface.
/// </summary>
public interface ISteamApiService
{
	/// <summary>
	/// The app id of the game.
	/// </summary>
	int? AppId { get; }

	/// <summary>
	/// Is the api is initialized?
	/// </summary>
	bool Initialized { get; }

	/// <summary>
	/// Have the current user stats been requested?
	/// </summary>
	bool StatsRequested { get; }

	/// <summary>
	/// This will set up the global state and populate the interface pointers which are accessible via
	/// the global functions which match the name of the interface.
	/// </summary>
	/// <remarks>
	/// This <u>MUST</u> be called and return successfully prior to accessing any of the Steamworks Interfaces!
	/// </remarks>
	/// <param name="appId">The Steamworks API will not initialize if it does not know the App ID of your game.</param>
	/// <returns>True, if the api is initialized, otherwise false.</returns>
	bool Initialize(int appId);

	/// <summary>
	/// When done using the Steamworks API you should call <see cref="Shutdown"/> to release the resources used
	/// by your application internally within Steam. You should call this during process shutdown if possible.
	/// </summary>
	void Shutdown();

	/// <summary>
	/// Resets the unlock status of an achievement.
	/// </summary>
	/// <param name="name">The 'API Name' of the Achievement to reset.</param>
	/// <returns>This function returns true upon success if all of the following conditions are met otherwise, false.</returns>
	bool ResetAchievement(string name);

	/// <summary>
	/// Gets the achievement status, and the time it was unlocked if unlocked.
	/// </summary>
	/// <param name="name">The 'API Name' of the achievement.</param>
	/// <returns>Returns whether the the achievement was unlocked and when.</returns>
	(bool Achieved, DateTime? UnlockTime) GetAchievement(string name);

	/// <summary>
	/// Unlocks an achievement.
	/// </summary>
	/// <param name="name">The 'API Name' of the Achievement to unlock.</param>
	/// <returns>True, if the achievement was unlocked, otherwise false.</returns>
	bool UnlockAchievement(string name);

	/// <summary>
	/// Request the user's current stats and achievements from the server.
	/// </summary>
	/// <remarks>
	/// You must always call this first to get the initial status of stats and achievements.
	/// </remarks>
	/// <returns>True, if the request was successful, otherwise false.</returns>
	bool RequestStats();

	/// <summary>
	/// Send the changed stats and achievements data to the server for permanent storage.
	/// </summary>
	/// <remarks>
	/// If this fails then nothing is sent to the server. It's advisable to keep trying until the call is successful.
	/// </remarks>
	/// <returns>True, if the request was successful, otherwise false.</returns>
	bool StoreStats();
}
