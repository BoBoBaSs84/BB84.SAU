// -----------------------------------------------------------------------------
// Copyright:	Robert Peter Meyer
// License:		MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------------
using Steamworks;

namespace BB84.SAU.Infrastructure.Interfaces.Provider;

/// <summary>
/// The steam works provider interface.
/// </summary>
public interface ISteamWorksProvider
{
	/// <inheritdoc cref="SteamUserStats.ClearAchievement(string)"/>
	bool ClearAchievement(string pchName);

	/// <inheritdoc cref="SteamUserStats.GetAchievementAndUnlockTime(string, out bool, out uint)"/>
	bool GetAchievementAndUnlockTime(string pchName, out bool pbAchieved, out uint punUnlockTime);

	/// <inheritdoc cref="SteamAPI.Init"/>
	bool Init();

	/// <inheritdoc cref="SteamUserStats.RequestCurrentStats"/>
	bool RequestCurrentStats();

	/// <inheritdoc cref="SetAchievement(string)"/>
	bool SetAchievement(string pchName);

	/// <inheritdoc cref="StoreStats"/>
	bool StoreStats();

	/// <inheritdoc cref="SteamAPI.Shutdown"/>
	void Shutdown();
}
