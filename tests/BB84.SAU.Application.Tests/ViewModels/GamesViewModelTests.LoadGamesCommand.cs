// -----------------------------------------------------------------------------
// Copyright:	Robert Peter Meyer
// License:		MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------------
using BB84.SAU.Application.ViewModels;
using BB84.SAU.Domain.Models;
using BB84.SAU.Domain.Settings;

using Moq;

namespace BB84.SAU.Application.Tests.ViewModels;

public sealed partial class GamesViewModelTests
{
	[TestMethod]
	[TestCategory("Commands")]
	public async Task LoadGamesCommandShouldLoadGamesWhenLastUpdateIsSet()
	{
		int appId = 1;
		string appTitle = "Fancy";
		GameDetailModel game = new(appId, appTitle);
		List<GameModel> games = [new(appId, appTitle)];
		GamesViewModel viewModel = CreateViewModelMock();
		_ = _optionsMock.Setup(x => x.Value).Returns(SteamSettings);
		_ = _steamWebServiceMock.Setup(x => x.GetGamesAsync(SteamSettings.Id, SteamSettings.ApiKey, default)).ReturnsAsync(games);
		_ = _steamWebServiceMock.Setup(x => x.GetGameDetailsAsync(appId, default)).ReturnsAsync(game);
		viewModel.Model.LastUpdate = DateTime.MinValue;

		await viewModel.LoadGamesCommand.ExecuteAsync()
			.ConfigureAwait(false);

		Assert.IsFalse(viewModel.GamesAreLoading);
	}

	[TestMethod]
	[TestCategory("Commands")]
	public async Task LoadGamesCommandShouldNotLoadGamesWhenLastUpdateIsNull()
	{
		GamesViewModel viewModel = CreateViewModelMock();
		viewModel.Model.LastUpdate = null;

		await viewModel.LoadGamesCommand.ExecuteAsync()
			.ConfigureAwait(false);

		Assert.IsFalse(viewModel.GamesAreLoading);
	}
}
