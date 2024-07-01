using BB84.SAU.Application.Interfaces.Application.Services;
using BB84.SAU.Application.Interfaces.Infrastructure.Persistence;
using BB84.SAU.Application.Interfaces.Infrastructure.Services;
using BB84.SAU.Infrastructure.Interfaces.Provider;
using BB84.SAU.Infrastructure.Persistence;

using Moq;

namespace BB84.SAU.Infrastructure.Tests.Persistence;

[TestClass]
public sealed partial class UserDataServiceTests
{
	private const string UserDataContent = @"{""name"":""TestUser"",""imageUrl"":""TestUser"",""profileUrl"":""TestUser"",""created"":""0001-01-01T00:00:00"",""lastLogOff"":""0001-01-01T00:00:00"",""games"":[]}";
	private Mock<ILoggerService<UserDataService>> _loggerServiceMock = new();
	private Mock<INotificationService> _notificationServiceMock = new();
	private Mock<IFileProvider> _fileProviderMock = new();

	[TestMethod]
	[TestCategory("Constructor")]
	public void UserDataServiceTest()
	{
		IUserDataService? userDataService;

		userDataService = CreateMockedInstance();

		Assert.IsNotNull(userDataService);
	}

	/// <summary>
	/// Creates a new instance of the <see cref="UserDataService"/> class with mocked dependencies.
	/// </summary>
	/// <returns>The new instance with mocked dependencies.</returns>
	private UserDataService CreateMockedInstance()
	{
		_loggerServiceMock = new();
		_notificationServiceMock = new();
		_fileProviderMock = new();

		return new(_loggerServiceMock.Object, _notificationServiceMock.Object, _fileProviderMock.Object);
	}
}
