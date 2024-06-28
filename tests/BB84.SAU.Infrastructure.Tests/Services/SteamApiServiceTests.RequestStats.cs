using BB84.SAU.Domain.Exceptions;
using BB84.SAU.Infrastructure.Services;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BB84.SAU.Infrastructure.Tests.Services;

public sealed partial class SteamApiServiceTests
{
	[TestMethod]
	[TestCategory("Methods")]
	public void RequestStatsShouldReturnFalseWhenNotInitialized()
	{
		SteamApiService service = CreateMockedService();

		bool result = service.RequestCurrentStats();

		Assert.IsFalse(result);
		Assert.IsFalse(service.StatsRequested);
		Assert.AreEqual(1, _loggerServiceMock.Invocations.Count);
		Assert.AreEqual(1, _notificationServiceMock.Invocations.Count);
	}

	[TestMethod]
	[TestCategory("Methods")]
	public void RequestStatsShouldReturnFalseWhenSteamWorksProviderReturnedFalse()
	{
		SteamApiService service = CreateMockedService();
		_steamWorksProviderMock.Setup(x => x.Init()).Returns(true);
		_steamWorksProviderMock.Setup(x => x.RequestCurrentStats()).Returns(false);
		service.Init(AppId);

		bool result = service.RequestCurrentStats();

		Assert.IsFalse(result);
		Assert.IsFalse(service.StatsRequested);
		Assert.AreEqual(0, _loggerServiceMock.Invocations.Count);
		Assert.AreEqual(0, _notificationServiceMock.Invocations.Count);
	}

	[TestMethod]
	[TestCategory("Methods")]
	public void RequestStatsShouldReturnTrueWhenSteamWorksProviderReturnedTrue()
	{
		SteamApiService service = CreateMockedService();
		_steamWorksProviderMock.Setup(x => x.Init()).Returns(true);
		_steamWorksProviderMock.Setup(x => x.RequestCurrentStats()).Returns(true);
		service.Init(AppId);

		bool result = service.RequestCurrentStats();

		Assert.IsTrue(result);
		Assert.IsTrue(service.StatsRequested);
		Assert.AreEqual(0, _loggerServiceMock.Invocations.Count);
		Assert.AreEqual(0, _notificationServiceMock.Invocations.Count);
	}

	[TestMethod]
	[TestCategory("Methods")]
	public void RequestStatsShouldReturnFalseWhenExceptionGetThrown()
	{
		SteamApiService service = CreateMockedService();
		_steamWorksProviderMock.Setup(x => x.Init()).Returns(true);
		_steamWorksProviderMock.Setup(x => x.RequestCurrentStats()).Throws(new SteamSdkException("Failure"));
		service.Init(AppId);

		bool result = service.RequestCurrentStats();

		Assert.IsFalse(result);
		Assert.IsFalse(service.StatsRequested);
		Assert.AreEqual(1, _loggerServiceMock.Invocations.Count);
		Assert.AreEqual(1, _notificationServiceMock.Invocations.Count);
	}
}
