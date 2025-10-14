// -----------------------------------------------------------------------------
// Copyright:	Robert Peter Meyer
// License:		MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------------
using BB84.SAU.Domain.Models;
using BB84.SAU.Infrastructure.Persistence;

using Moq;

namespace BB84.SAU.Infrastructure.Tests.Persistence;

[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, unit testing.")]
public sealed partial class UserDataServiceTests
{
	[TestMethod]
	[TestCategory("Methods")]
	public async Task LoadUserDataShouldReturnNewWhenExceptionGetsThrown()
	{
		UserDataService service = CreateMockedInstance();
		_fileProviderMock.Setup(x => x.Exists(It.IsAny<string>())).Returns(true);
		_fileProviderMock.Setup(x => x.ReadAllTextAsync(It.IsAny<string>(), default))
			.Throws(new IOException());

		UserDataModel result = await service.LoadUserDataAsync();

		Assert.IsNotNull(result);
		Assert.HasCount(1, _loggerServiceMock.Invocations);
		Assert.HasCount(2, _fileProviderMock.Invocations);
	}

	[TestMethod]
	[TestCategory("Methods")]
	public async Task LoadUserDataShouldReturnNewWhenNotExists()
	{
		UserDataService service = CreateMockedInstance();
		_fileProviderMock.Setup(x => x.Exists(It.IsAny<string>())).Returns(false);

		UserDataModel result = await service.LoadUserDataAsync();

		Assert.IsNotNull(result);
		Assert.HasCount(1, _fileProviderMock.Invocations);
	}

	[TestMethod]
	[TestCategory("Methods")]
	public async Task LoadUserDataShouldReturnValidResultWhenSuccessful()
	{
		string testUser = "UnitTest";
		DateTime testDate = new(1, 1, 1);
		UserDataService service = CreateMockedInstance();
		_fileProviderMock.Setup(x => x.Exists(It.IsAny<string>())).Returns(true);
		_fileProviderMock.Setup(x => x.ReadAllTextAsync(It.IsAny<string>(), default))
			.ReturnsAsync(UserDataContent);

		UserDataModel result = await service.LoadUserDataAsync();

		Assert.IsNotNull(result);
		Assert.AreEqual(testUser, result.Name);
		Assert.AreEqual(testUser, result.ProfileUrl);
		Assert.AreEqual(testUser, result.ImageUrl);
		Assert.AreEqual(testDate, result.Created);
		Assert.AreEqual(testDate, result.LastLogOff);
		Assert.IsNull(result.LastUpdate);
		Assert.IsEmpty(_loggerServiceMock.Invocations);
		Assert.HasCount(2, _fileProviderMock.Invocations);
	}
}
