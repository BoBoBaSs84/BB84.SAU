using BB84.SAU.Application.Interfaces.Application.Provider;
using BB84.SAU.Application.Interfaces.Application.Services;
using BB84.SAU.Application.Interfaces.Infrastructure.Services;
using BB84.SAU.Application.ViewModels;
using BB84.SAU.Domain.Models;
using BB84.SAU.Domain.Settings;

using Microsoft.Extensions.Options;

using Moq;

namespace BB84.SAU.Application.Tests.ViewModels;

[TestClass]
public sealed partial class GamesViewModelTests : ApplicationTestBase
{
	private static readonly SteamSettings SteamSettings = new() { Id = 0, ApiKey = "TestKey" };
	private Mock<IDateTimeProvider> _dateTimeProviderMock = new();
	private Mock<INavigationService> _navigationServiceMock = new();
	private Mock<ISteamApiService> _steamApiServiceMock = new();
	private Mock<ISteamWebService> _steamWebServiceMock = new();
	private Mock<IOptions<SteamSettings>> _optionsMock = new();

	[TestMethod]
	[TestCategory("Constructor")]
	public void GamesViewModelTest()
	{
		GamesViewModel gamesViewModel = CreateViewModelMock();

		Assert.IsNotNull(gamesViewModel);
		Assert.IsNotNull(gamesViewModel.Model);
		Assert.IsNull(gamesViewModel.SelectedGame);
		Assert.IsNull(gamesViewModel.GameImage);
		Assert.IsFalse(gamesViewModel.LoadGamesCommand.CanExecute());
		Assert.IsFalse(gamesViewModel.GamesAreLoading);
		Assert.IsFalse(gamesViewModel.IsGameVisible);
		Assert.IsFalse(gamesViewModel.IsSelectButtonVisible);
	}

	private GamesViewModel CreateViewModelMock()
	{
		_dateTimeProviderMock = new();
		_navigationServiceMock = new();
		_steamApiServiceMock = new();
		_steamWebServiceMock = new();
		_optionsMock = new();
		_ = _optionsMock.Setup(x => x.Value).Returns(SteamSettings);

		AchievementsViewModel achievementsViewModel =
			new(_steamApiServiceMock.Object, _steamWebServiceMock.Object, _optionsMock.Object, _dateTimeProviderMock.Object);

		UserDataModel userDataModel = new();

		return new(_navigationServiceMock.Object, _steamWebServiceMock.Object, _optionsMock.Object, achievementsViewModel, userDataModel);
	}
}
