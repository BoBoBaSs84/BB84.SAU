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
	public void StoreStatsShouldReturnFalseWhenStatsNotRequested()
	{
		SteamApiService service = CreateMockedService();
		_steamWorksProviderMock.Setup(x => x.Init()).Returns(true);
		service.Initialize(AppId);

		bool result = service.StoreStats();

		Assert.IsFalse(result);
		Assert.HasCount(1, _loggerServiceMock.Invocations);
		Assert.HasCount(2, _notificationServiceMock.Invocations);
	}

	[TestMethod]
	[TestCategory("Methods")]
	public void StoreStatsShouldReturnFalseWhenStoreStatsReturnedFalse()
	{
		SteamApiService service = CreateMockedService();
		_steamWorksProviderMock.Setup(x => x.Init()).Returns(true);
		_steamWorksProviderMock.Setup(x => x.RequestCurrentStats()).Returns(true);
		_steamWorksProviderMock.Setup(x => x.StoreStats()).Returns(false);
		service.Initialize(AppId);
		service.RequestStats();

		bool result = service.StoreStats();

		Assert.IsFalse(result);
		Assert.IsEmpty(_loggerServiceMock.Invocations);
		Assert.HasCount(2, _notificationServiceMock.Invocations);
	}

	[TestMethod]
	[TestCategory("Methods")]
	public void StoreStatsShouldReturnTrueWhenStoreStatsReturnedTrue()
	{
		SteamApiService service = CreateMockedService();
		_steamWorksProviderMock.Setup(x => x.Init()).Returns(true);
		_steamWorksProviderMock.Setup(x => x.RequestCurrentStats()).Returns(true);
		_steamWorksProviderMock.Setup(x => x.StoreStats()).Returns(true);
		service.Initialize(AppId);
		service.RequestStats();

		bool result = service.StoreStats();

		Assert.IsTrue(result);
		Assert.IsEmpty(_loggerServiceMock.Invocations);
		Assert.HasCount(3, _notificationServiceMock.Invocations);
	}
}
