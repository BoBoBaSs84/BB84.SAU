// -----------------------------------------------------------------------------
// Copyright:	Robert Peter Meyer
// License:		MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------------
using BB84.SAU.Domain.Exceptions;
using BB84.SAU.Infrastructure.Services;

namespace BB84.SAU.Infrastructure.Tests.Services;

[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, unit testing.")]
public sealed partial class SteamApiServiceTests
{
	[TestMethod]
	[TestCategory("Methods")]
	public void RequestStatsShouldReturnFalseWhenNotInitialized()
	{
		SteamApiService service = CreateMockedService();

		bool result = service.RequestStats();

		Assert.IsFalse(result);
		Assert.IsFalse(service.StatsRequested);
		Assert.HasCount(1, _loggerServiceMock.Invocations);
		Assert.HasCount(1, _notificationServiceMock.Invocations);
	}

	[TestMethod]
	[TestCategory("Methods")]
	public void RequestStatsShouldReturnFalseWhenSteamWorksProviderReturnedFalse()
	{
		SteamApiService service = CreateMockedService();
		_steamWorksProviderMock.Setup(x => x.Init()).Returns(true);
		_steamWorksProviderMock.Setup(x => x.RequestCurrentStats()).Returns(false);
		service.Initialize(AppId);

		bool result = service.RequestStats();

		Assert.IsFalse(result);
		Assert.IsFalse(service.StatsRequested);
		Assert.IsEmpty(_loggerServiceMock.Invocations);
		Assert.HasCount(2, _notificationServiceMock.Invocations);
	}

	[TestMethod]
	[TestCategory("Methods")]
	public void RequestStatsShouldReturnTrueWhenSteamWorksProviderReturnedTrue()
	{
		SteamApiService service = CreateMockedService();
		_steamWorksProviderMock.Setup(x => x.Init()).Returns(true);
		_steamWorksProviderMock.Setup(x => x.RequestCurrentStats()).Returns(true);
		service.Initialize(AppId);

		bool result = service.RequestStats();

		Assert.IsTrue(result);
		Assert.IsTrue(service.StatsRequested);
		Assert.IsEmpty(_loggerServiceMock.Invocations);
		Assert.HasCount(2, _notificationServiceMock.Invocations);
	}

	[TestMethod]
	[TestCategory("Methods")]
	public void RequestStatsShouldReturnFalseWhenExceptionGetThrown()
	{
		SteamApiService service = CreateMockedService();
		_steamWorksProviderMock.Setup(x => x.Init()).Returns(true);
		_steamWorksProviderMock.Setup(x => x.RequestCurrentStats()).Throws(new SteamSdkException("Failure"));
		service.Initialize(AppId);

		bool result = service.RequestStats();

		Assert.IsFalse(result);
		Assert.IsFalse(service.StatsRequested);
		Assert.HasCount(1, _loggerServiceMock.Invocations);
		Assert.HasCount(2, _notificationServiceMock.Invocations);
	}
}
