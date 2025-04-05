// -----------------------------------------------------------------------------
// Copyright:	Robert Peter Meyer
// License:		MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------------
using System.Diagnostics.CodeAnalysis;

using BB84.SAU.Infrastructure.Interfaces.Provider;

using Steamworks;

namespace BB84.SAU.Infrastructure.Provider;

/// <summary>
/// The steam works provider class.
/// </summary>
[ExcludeFromCodeCoverage(Justification = "Wrapper/Astraction class")]
internal sealed class SteamWorksProvider : ISteamWorksProvider
{
	public bool ClearAchievement(string pchName)
		=> SteamUserStats.ClearAchievement(pchName);

	public bool GetAchievementAndUnlockTime(string pchName, out bool pbAchieved, out uint punUnlockTime)
		=> SteamUserStats.GetAchievementAndUnlockTime(pchName, out pbAchieved, out punUnlockTime);

	public bool Init()
		=> SteamAPI.Init();

	public bool RequestCurrentStats()
		=> SteamUserStats.RequestCurrentStats();

	public bool SetAchievement(string pchName)
		=> SteamUserStats.SetAchievement(pchName);

	public void Shutdown()
		=> SteamAPI.Shutdown();

	public bool StoreStats()
		=> SteamUserStats.StoreStats();
}
