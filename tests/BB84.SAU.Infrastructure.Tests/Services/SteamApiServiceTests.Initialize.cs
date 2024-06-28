using System.Diagnostics.CodeAnalysis;

using BB84.SAU.Domain.Exceptions;
using BB84.SAU.Infrastructure.Services;

using Microsoft.VisualStudio.TestTools.UnitTesting;

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
		Assert.AreEqual(1, _fileProviderMock.Invocations.Count);
		Assert.AreEqual(0, _notificationServiceMock.Invocations.Count);
		Assert.AreEqual(0, _loggerServiceMock.Invocations.Count);
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
		Assert.AreEqual(1, _fileProviderMock.Invocations.Count);
		Assert.AreEqual(0, _notificationServiceMock.Invocations.Count);
		Assert.AreEqual(0, _loggerServiceMock.Invocations.Count);
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
		Assert.IsTrue(_loggerServiceMock.Invocations.Count == 1);
		Assert.IsTrue(_notificationServiceMock.Invocations.Count == 1);
	}
}
