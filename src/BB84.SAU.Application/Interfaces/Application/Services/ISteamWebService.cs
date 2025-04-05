// -----------------------------------------------------------------------------
// Copyright:	Robert Peter Meyer
// License:		MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------------
using BB84.SAU.Domain.Models;

namespace BB84.SAU.Application.Interfaces.Application.Services;

/// <summary>
/// The steam web service interface.
/// </summary>
public interface ISteamWebService
{
	/// <summary>
	/// Returns the complete list of stats and achievements for the specified game.
	/// </summary>
	/// <param name="appId">The steam app id to use.</param>
	/// <param name="apiKey">The steam api key to use.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns>A list of achievements.</returns>
	Task<IEnumerable<AchievementModel>> GetAchievementsAsync(int appId, string apiKey, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns a list of games a player owns along with some playtime information,
	/// if the profile is publicly visible.
	/// </summary>
	/// <param name="steamId">The user steam id to use.</param>
	/// <param name="apiKey">The steam api key to use.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns>A list of games.</returns>
	Task<IEnumerable<GameModel>> GetGamesAsync(long steamId, string apiKey, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns the application details for a given <paramref name="appId"/>.
	/// </summary>
	/// <param name="appId">The steam app id to use.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns>The game with some more details.</returns>
	Task<GameDetailModel?> GetGameDetailsAsync(int appId, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns basic profile user data information.
	/// </summary>
	/// <param name="steamId">The user steam id to use.</param>
	/// <param name="apiKey">The steam api key to use.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns>The profile user data.</returns>
	Task<UserDataModel?> GetUserDataAsync(long steamId, string apiKey, CancellationToken cancellationToken = default);
}
