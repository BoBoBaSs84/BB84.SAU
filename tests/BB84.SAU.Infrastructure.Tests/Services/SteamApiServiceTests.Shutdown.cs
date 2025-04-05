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

		Assert.AreEqual(0, _fileProviderMock.Invocations.Count);
		Assert.AreEqual(0, _steamWorksProviderMock.Invocations.Count);
		Assert.AreEqual(1, _loggerServiceMock.Invocations.Count);
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
		Assert.AreEqual(2, _fileProviderMock.Invocations.Count);
		Assert.AreEqual(2, _steamWorksProviderMock.Invocations.Count);
		Assert.AreEqual(0, _loggerServiceMock.Invocations.Count);
	}
}
