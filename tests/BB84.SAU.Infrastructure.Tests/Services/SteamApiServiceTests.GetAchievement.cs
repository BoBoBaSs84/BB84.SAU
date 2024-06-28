using BB84.SAU.Infrastructure.Services;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BB84.SAU.Infrastructure.Tests.Services;

public sealed partial class SteamApiServiceTests
{
	[TestMethod]
	[TestCategory("Methods")]
	public void GetAchievementShouldReturnFalseWhenStatsNotRequested()
	{
		SteamApiService service = CreateMockedService();

		(bool achieved, DateTime? unlockTime) = service.GetAchievement(AchievementName);

		Assert.IsFalse(achieved);
		Assert.IsNull(unlockTime);
		Assert.AreEqual(1, _loggerServiceMock.Invocations.Count);
		Assert.AreEqual(1, _notificationServiceMock.Invocations.Count);
		Assert.AreEqual(0, _steamWorksProviderMock.Invocations.Count);
	}

	[TestMethod]
	[TestCategory("Methods")]
	public void GetAchievementShouldReturnFalseWhenAchievementNotUnlocked()
	{
		uint unlockTime = 0;
		bool achieved = false;
		SteamApiService service = CreateMockedService();
		_steamWorksProviderMock.Setup(x => x.Init()).Returns(true);
		_steamWorksProviderMock.Setup(x => x.RequestCurrentStats()).Returns(true);
		_steamWorksProviderMock.Setup(x => x.GetAchievementAndUnlockTime(AchievementName, out achieved, out unlockTime)).Returns(true);
		service.Init(AppId);
		service.RequestCurrentStats();

		(bool Achieved, DateTime? UnlockTime) result = service.GetAchievement(AchievementName);

		Assert.IsFalse(result.Achieved);
		Assert.IsNull(result.UnlockTime);
		Assert.AreEqual(0, _loggerServiceMock.Invocations.Count);
		Assert.AreEqual(0, _notificationServiceMock.Invocations.Count);
		Assert.AreEqual(3, _steamWorksProviderMock.Invocations.Count);
	}

	[TestMethod]
	[TestCategory("Methods")]
	public void GetAchievementShouldReturnValuesWhenSteamWorksProviderReturnedValues()
	{
		uint unlockTime = 946681200;
		bool achieved = true;
		SteamApiService service = CreateMockedService();
		_steamWorksProviderMock.Setup(x => x.Init()).Returns(true);
		_steamWorksProviderMock.Setup(x => x.RequestCurrentStats()).Returns(true);
		_steamWorksProviderMock.Setup(x => x.GetAchievementAndUnlockTime(AchievementName, out achieved, out unlockTime));

		service.Init(AppId);
		service.RequestCurrentStats();

		(bool Achieved, DateTime? UnlockTime) result = service.GetAchievement(AchievementName);

		Assert.IsTrue(result.Achieved);
		Assert.AreEqual(new DateTime(2000, 1, 1), result.UnlockTime);
		Assert.AreEqual(0, _loggerServiceMock.Invocations.Count);
		Assert.AreEqual(0, _notificationServiceMock.Invocations.Count);
		Assert.AreEqual(3, _steamWorksProviderMock.Invocations.Count);
	}
}
