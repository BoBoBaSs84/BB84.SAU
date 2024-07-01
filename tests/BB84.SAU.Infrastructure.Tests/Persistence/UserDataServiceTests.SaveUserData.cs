using BB84.SAU.Domain.Models;
using BB84.SAU.Infrastructure.Persistence;

using Moq;

namespace BB84.SAU.Infrastructure.Tests.Persistence;

[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, unit testing.")]
public sealed partial class UserDataServiceTests
{
	[TestMethod]
	[TestCategory("Methods")]
	public async Task SaveUserDataShouldReturnFalseWhenExceptionGetsThrown()
	{
		UserDataService service = CreateMockedInstance();
		_fileProviderMock.Setup(x => x.WriteAllTextAsync(It.IsAny<string>(), It.IsAny<string>(), default))
			.Throws(new IOException());

		bool result = await service.SaveUserDataAsync(null!);

		Assert.IsFalse(result);
		Assert.AreEqual(1, _loggerServiceMock.Invocations.Count);
		Assert.AreEqual(1, _notificationServiceMock.Invocations.Count);
	}

	[TestMethod]
	[TestCategory("Methods")]
	public async Task SaveUserDataShouldReturnTrueWhenSuccessful()
	{
		UserDataService service = CreateMockedInstance();
		_fileProviderMock.Setup(x => x.WriteAllTextAsync(It.IsAny<string>(), It.IsAny<string>(), default))
			.Returns(Task.CompletedTask);
		string unitTest = "UnitTest";
		DateTime dateTime = new(1970, 1, 1);
		UserDataModel userData = new(unitTest, unitTest, unitTest, dateTime, dateTime, dateTime);

		bool result = await service.SaveUserDataAsync(userData);

		Assert.IsTrue(result);
		Assert.AreEqual(0, _loggerServiceMock.Invocations.Count);
		Assert.AreEqual(1, _notificationServiceMock.Invocations.Count);
		Assert.AreEqual(1, _fileProviderMock.Invocations.Count);
	}
}
