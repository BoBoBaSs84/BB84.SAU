using BB84.SAU.Application.ViewModels;
using BB84.SAU.Domain.Models;
using BB84.SAU.Domain.Settings;

using Microsoft.VisualStudio.TestTools.UnitTesting;

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
		_optionsMock.Setup(x => x.Value).Returns(SteamSettings);
		_steamWebServiceMock.Setup(x => x.GetGames(SteamSettings.Id, SteamSettings.ApiKey, default)).ReturnsAsync(games);
		_steamWebServiceMock.Setup(x => x.GetGameDetails(appId, default)).ReturnsAsync(game);
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
