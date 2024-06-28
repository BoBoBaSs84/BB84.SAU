using System.Diagnostics.CodeAnalysis;

using BB84.SAU.Infrastructure.Services;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BB84.SAU.Infrastructure.Tests.Services;

[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, unit testing.")]
public sealed partial class SteamApiServiceTests
{
	[TestMethod]
	[TestCategory("Methods")]
	public void ResetAchievementShouldReturnFalseWhenStatsNotRequested()
	{
		SteamApiService service = CreateMockedService();
		_steamWorksProviderMock.Setup(x => x.Init()).Returns(true);
		service.Initialize(AppId);

		bool result = service.ResetAchievement(AchievementName);

		Assert.IsFalse(result);
		Assert.AreEqual(1, _loggerServiceMock.Invocations.Count);
		Assert.AreEqual(1, _notificationServiceMock.Invocations.Count);
	}

	[TestMethod]
	[TestCategory("Methods")]
	public void ResetAchievementShouldReturnFalseWhenNotFound()
	{
		SteamApiService service = CreateMockedService();
		_steamWorksProviderMock.Setup(x => x.Init()).Returns(true);
		_steamWorksProviderMock.Setup(x => x.RequestCurrentStats()).Returns(true);
		_steamWorksProviderMock.Setup(x => x.ClearAchievement(AchievementName)).Returns(false);
		service.Initialize(AppId);
		service.RequestStats();

		bool result = service.ResetAchievement(AchievementName);

		Assert.IsFalse(result);
		Assert.AreEqual(0, _loggerServiceMock.Invocations.Count);
		Assert.AreEqual(0, _notificationServiceMock.Invocations.Count);
	}

	[TestMethod]
	[TestCategory("Methods")]
	public void ResetAchievementShouldReturnTrueWhenFound()
	{
		SteamApiService service = CreateMockedService();
		_steamWorksProviderMock.Setup(x => x.Init()).Returns(true);
		_steamWorksProviderMock.Setup(x => x.RequestCurrentStats()).Returns(true);
		_steamWorksProviderMock.Setup(x => x.ClearAchievement(AchievementName)).Returns(true);
		service.Initialize(AppId);
		service.RequestStats();

		bool result = service.ResetAchievement(AchievementName);

		Assert.IsTrue(result);
		Assert.AreEqual(0, _loggerServiceMock.Invocations.Count);
		Assert.AreEqual(0, _notificationServiceMock.Invocations.Count);
	}
}
