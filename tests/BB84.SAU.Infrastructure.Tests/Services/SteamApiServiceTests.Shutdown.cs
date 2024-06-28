using BB84.SAU.Infrastructure.Services;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BB84.SAU.Infrastructure.Tests.Services;

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
		service.Init(1);

		service.Shutdown();

		Assert.IsNull(service.AppId);
		Assert.AreEqual(2, _fileProviderMock.Invocations.Count);
		Assert.AreEqual(2, _steamWorksProviderMock.Invocations.Count);
		Assert.AreEqual(0, _loggerServiceMock.Invocations.Count);
	}
}
