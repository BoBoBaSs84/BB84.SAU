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
	public void ShutdownShouldThrowExceptionWhenNotInitialized()
	{
		SteamApiService service = CreateMockedService();

		service.Shutdown();

		Assert.IsEmpty(_fileProviderMock.Invocations);
		Assert.IsEmpty(_steamWorksProviderMock.Invocations);
		Assert.HasCount(1, _loggerServiceMock.Invocations);
	}

	[TestMethod]
	[TestCategory("Methods")]
	public void ShutdownShouldSuccedWhenInitialized()
	{
		SteamApiService service = CreateMockedService();
		_steamWorksProviderMock.Setup(x => x.Init()).Returns(true);
		service.Initialize(AppId);

		service.Shutdown();

		Assert.IsNull(service.AppId);
		Assert.HasCount(2, _fileProviderMock.Invocations);
		Assert.HasCount(2, _steamWorksProviderMock.Invocations);
		Assert.IsEmpty(_loggerServiceMock.Invocations);
	}
}
