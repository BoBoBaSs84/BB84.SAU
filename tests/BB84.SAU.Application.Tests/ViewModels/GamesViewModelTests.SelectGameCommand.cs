using BB84.SAU.Application.ViewModels;
using BB84.SAU.Domain.Models;

namespace BB84.SAU.Application.Tests.ViewModels;

public sealed partial class GamesViewModelTests
{
	[TestMethod]
	[TestCategory("Commands")]
	public void SelectGameCommandShouldNotSetSelectedGameWhenLastUpdateIsNull()
	{
		GamesViewModel viewModel = CreateViewModelMock();
		GameDetailModel game = new(1, "Fancy");

		viewModel.SelectGameCommand.Execute(game);

		Assert.AreNotEqual(game, viewModel.SelectedGame);
	}

	[ViewModelTest]
	[TestCategory("Commands")]
	public void SelectGameCommandShouldSetSelectedGameWhenLastUpdateIsSet()
	{
		string imageUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/3/32/Telefunken_FuBK_test_pattern.svg/320px-Telefunken_FuBK_test_pattern.svg.png";
		GamesViewModel viewModel = CreateViewModelMock();
		viewModel.SelectedGame = new(1, "Fancy", string.Empty, imageUrl, DateTime.MinValue);

		viewModel.SelectGameCommand.Execute(viewModel.SelectedGame);

		Assert.IsTrue(viewModel.IsGameVisible);
		Assert.IsTrue(viewModel.IsSelectButtonVisible);
	}
}
