using BB84.SAU.Application.Interfaces.Application.Services;
using BB84.SAU.Application.Interfaces.Infrastructure.Services;
using BB84.SAU.Infrastructure.Interfaces.Provider;
using BB84.SAU.Infrastructure.Services;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace BB84.SAU.Infrastructure.Tests.Services;

[TestClass]
public sealed partial class SteamApiServiceTests
{
	private const int AppId = 1;
	private const string AchievementName = "TestAchievement";
	private Mock<ILoggerService<SteamApiService>> _loggerServiceMock = new();
	private Mock<INotificationService> _notificationServiceMock = new();
	private Mock<ISteamWorksProvider> _steamWorksProviderMock = new();
	private Mock<IFileProvider> _fileProviderMock = new();

	[TestMethod]
	[TestCategory("Constructor")]
	public void SteamApiServiceTest()
	{
		SteamApiService service = CreateMockedService();

		Assert.IsNotNull(service);
		Assert.IsNull(service.AppId);
		Assert.IsFalse(service.Initialized);
		Assert.IsFalse(service.StatsRequested);
	}

	/// <summary>
	/// Creates a new instance of the <see cref="SteamApiService"/> class with mocked dependencies.
	/// </summary>
	/// <returns>The new instance with mocked dependencies.</returns>
	private SteamApiService CreateMockedService()
	{
		_loggerServiceMock = new();
		_steamWorksProviderMock = new();
		_fileProviderMock = new();
		_notificationServiceMock = new();

		return new SteamApiService(
			_loggerServiceMock.Object,
			_notificationServiceMock.Object,
			_steamWorksProviderMock.Object,
			_fileProviderMock.Object
			);
	}
}
