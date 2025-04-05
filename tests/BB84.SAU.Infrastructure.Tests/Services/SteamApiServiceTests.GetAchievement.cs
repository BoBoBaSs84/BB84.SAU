// -----------------------------------------------------------------------------
// Copyright:	Robert Peter Meyer
// License:		MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------------
using BB84.SAU.Infrastructure.Services;

namespace BB84.SAU.Infrastructure.Tests.Services;

[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, unit testing.")]
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
		service.Initialize(AppId);
		service.RequestStats();

		(bool Achieved, DateTime? UnlockTime) result = service.GetAchievement(AchievementName);

		Assert.IsFalse(result.Achieved);
		Assert.IsNull(result.UnlockTime);
		Assert.AreEqual(0, _loggerServiceMock.Invocations.Count);
		Assert.AreEqual(3, _notificationServiceMock.Invocations.Count);
		Assert.AreEqual(3, _steamWorksProviderMock.Invocations.Count);
	}

	[TestMethod]
	[TestCategory("Methods")]
	public void GetAchievementShouldReturnValuesWhenSteamWorksProviderReturnedValues()
	{
		uint unlockTime = 0;
		bool achieved = true;
		SteamApiService service = CreateMockedService();
		_steamWorksProviderMock.Setup(x => x.Init()).Returns(true);
		_steamWorksProviderMock.Setup(x => x.RequestCurrentStats()).Returns(true);
		_steamWorksProviderMock.Setup(x => x.GetAchievementAndUnlockTime(AchievementName, out achieved, out unlockTime));
		service.Initialize(AppId);
		service.RequestStats();

		(bool Achieved, DateTime? UnlockTime) result = service.GetAchievement(AchievementName);

		Assert.IsTrue(result.Achieved);
		Assert.AreEqual(DateTimeOffset.FromUnixTimeSeconds(unlockTime).LocalDateTime, result.UnlockTime);
		Assert.AreEqual(0, _loggerServiceMock.Invocations.Count);
		Assert.AreEqual(2, _notificationServiceMock.Invocations.Count);
		Assert.AreEqual(3, _steamWorksProviderMock.Invocations.Count);
	}
}
