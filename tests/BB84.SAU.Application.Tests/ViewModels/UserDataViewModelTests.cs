using BB84.SAU.Application.Interfaces.Application.Services;
using BB84.SAU.Application.ViewModels;
using BB84.SAU.Domain.Models;
using BB84.SAU.Domain.Settings;

using Microsoft.Extensions.Options;

using Moq;

namespace BB84.SAU.Application.Tests.ViewModels;

[TestClass]
public sealed class UserDataViewModelTests : ApplicationTestBase
{
	private const string TestImageUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/3/32/Telefunken_FuBK_test_pattern.svg/320px-Telefunken_FuBK_test_pattern.svg.png";

	[TestMethod]
	[TestCategory("Constructor")]
	public void UserDataViewModelTest()
	{
		Mock<ISteamWebService> steamWebApiServiceMock = new();
		Mock<IOptions<SteamSettings>> optionsMock = new();
		_ = optionsMock.Setup(x => x.Value).Returns(new SteamSettings());
		UserDataModel userDataModel = new();

		UserDataViewModel viewModel = new(steamWebApiServiceMock.Object, optionsMock.Object, userDataModel);

		Assert.IsNotNull(viewModel);
		Assert.IsNull(viewModel.Image);
		Assert.AreEqual(userDataModel, viewModel.Model);
		Assert.IsFalse(viewModel.IsUserDataLoading);
		Assert.IsFalse(viewModel.IsUserDataGridVisible);
		Assert.IsTrue(viewModel.IsUserDataLoadGridVisible);
		Assert.IsTrue(viewModel.LoadUserDataCommand.CanExecute());
	}

	[ViewModelTest]
	public async Task LoadUserDataCommandTest()
	{
		long id = long.MaxValue;
		string apiKey = Guid.NewGuid().ToString();
		UserDataModel userDataModel =
			new("UnitTest", TestImageUrl, "UnitTestUrl", DateTime.MinValue, DateTime.MinValue, DateTime.MinValue);
		Mock<ISteamWebService> steamWebApiServiceMock = new();
		_ = steamWebApiServiceMock.Setup(x => x.GetUserProfile(id, apiKey, default).Result).Returns(userDataModel);
		Mock<IOptions<SteamSettings>> optionsMock = new();
		_ = optionsMock.Setup(x => x.Value).Returns(new SteamSettings() { Id = id, ApiKey = apiKey });
		UserDataViewModel viewModel = new(steamWebApiServiceMock.Object, optionsMock.Object, new());

		await viewModel.LoadUserDataCommand.ExecuteAsync()
			.ConfigureAwait(false);

		Assert.IsNotNull(viewModel.Image);
		Assert.IsTrue(viewModel.IsUserDataGridVisible);
		Assert.IsFalse(viewModel.IsUserDataLoadGridVisible);
	}

	[ViewModelTest]
	public async Task LoadUserDataCommandReturnNullTest()
	{
		long id = long.MaxValue;
		string apiKey = Guid.NewGuid().ToString();
		UserDataModel userDataModel =
			new("UnitTest", TestImageUrl, "UnitTestUrl", DateTime.MinValue, DateTime.MinValue, DateTime.MinValue);
		Mock<ISteamWebService> steamWebApiServiceMock = new();
		_ = steamWebApiServiceMock.Setup(x => x.GetUserProfile(id, apiKey, default).Result);
		Mock<IOptions<SteamSettings>> optionsMock = new();
		_ = optionsMock.Setup(x => x.Value).Returns(new SteamSettings() { Id = id, ApiKey = apiKey });
		UserDataViewModel viewModel = new(steamWebApiServiceMock.Object, optionsMock.Object, new());

		await viewModel.LoadUserDataCommand.ExecuteAsync()
			.ConfigureAwait(false);

		Assert.IsNull(viewModel.Image);
		Assert.IsFalse(viewModel.IsUserDataGridVisible);
		Assert.IsTrue(viewModel.IsUserDataLoadGridVisible);
	}
}
