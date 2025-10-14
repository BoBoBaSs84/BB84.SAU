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
	public void InitializeShouldReturnTrueWhenSteamWorksProviderReturnedTrue()
	{
		SteamApiService service = CreateMockedService();
		_steamWorksProviderMock.Setup(x => x.Init()).Returns(true);

		bool result = service.Initialize(AppId);

		Assert.IsTrue(result);
		Assert.IsTrue(service.Initialized);
		Assert.AreEqual(AppId, service.AppId);
		Assert.HasCount(1, _fileProviderMock.Invocations);
		Assert.HasCount(1, _notificationServiceMock.Invocations);
		Assert.IsEmpty(_loggerServiceMock.Invocations);
	}

	[TestMethod]
	[TestCategory("Methods")]
	public void InitializeShouldReturnFalseWhenSteamWorksProviderReturnedFalse()
	{
		SteamApiService service = CreateMockedService();
		_steamWorksProviderMock.Setup(x => x.Init()).Returns(false);

		bool result = service.Initialize(AppId);

		Assert.IsFalse(result);
		Assert.IsFalse(service.Initialized);
		Assert.IsNull(service.AppId);
		Assert.HasCount(1, _fileProviderMock.Invocations);
		Assert.HasCount(1, _notificationServiceMock.Invocations);
		Assert.IsEmpty(_loggerServiceMock.Invocations);
	}

	[TestMethod]
	[TestCategory("Methods")]
	public void InitializeShouldReturnFalseWhenExceptionGetsThrown()
	{
		SteamApiService service = CreateMockedService();
		_steamWorksProviderMock.Setup(x => x.Init()).Throws(new SteamSdkException("Failure"));

		bool result = service.Initialize(AppId);

		Assert.IsFalse(result);
		Assert.IsFalse(service.Initialized);
		Assert.IsNull(service.AppId);
		Assert.HasCount(1, _loggerServiceMock.Invocations);
		Assert.HasCount(1, _notificationServiceMock.Invocations);
	}
}
