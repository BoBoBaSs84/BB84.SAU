// -----------------------------------------------------------------------------
// Copyright:	Robert Peter Meyer
// License:		MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------------
using System.Net;

using BB84.SAU.Application.Services;
using BB84.SAU.Domain.Models;

using Moq;

namespace BB84.SAU.Application.Tests.Services;

[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, unit testing.")]
public sealed partial class SteamWebServiceTests
{
	[TestMethod]
	[TestCategory("Methods")]
	public async Task GetUserDataShouldReturnNullWhenExceptionGetsThrown()
	{
		SteamWebService service = CreateMockedInstance();
		_httpClientFactoryMock.Setup(x => x.CreateClient(It.IsAny<string>())).Throws<HttpRequestException>();

		UserDataModel? result = await service.GetUserDataAsync(STEAMID, APIKEY)
			.ConfigureAwait(false);

		Assert.IsNull(result);
		Assert.HasCount(1, _loggerServiceMock.Invocations);
		Assert.HasCount(1, _notificationServiceMock.Invocations);
	}

	[TestMethod]
	[TestCategory("Methods")]
	public async Task GetUserDataShouldReturnNullWhenNotSuccessful()
	{
		SteamWebService service = CreateMockedInstance();
		_httpClientFactoryMock.Setup(x => x.CreateClient(It.IsAny<string>()))
			.Returns(CreateMockedClient(HttpStatusCode.NotFound));

		UserDataModel? result = await service.GetUserDataAsync(STEAMID, APIKEY)
			.ConfigureAwait(false);

		Assert.IsNull(result);
		Assert.HasCount(1, _httpClientFactoryMock.Invocations);
		Assert.HasCount(1, _httpMessageHandler.Invocations);
		Assert.HasCount(1, _loggerServiceMock.Invocations);
		Assert.HasCount(1, _notificationServiceMock.Invocations);
	}

	[TestMethod]
	[TestCategory("Methods")]
	public async Task GetUserDataShouldReturnResultWhenSuccessful()
	{
		SteamWebService service = CreateMockedInstance();
		_httpClientFactoryMock.Setup(x => x.CreateClient(It.IsAny<string>()))
			.Returns(CreateMockedClient(HttpStatusCode.OK, UserDataResponse));

		UserDataModel? result = await service.GetUserDataAsync(STEAMID, APIKEY)
			.ConfigureAwait(false);

		Assert.IsNotNull(result);
		Assert.HasCount(1, _httpClientFactoryMock.Invocations);
		Assert.HasCount(1, _httpMessageHandler.Invocations);
		Assert.IsEmpty(_loggerServiceMock.Invocations);
		Assert.HasCount(1, _notificationServiceMock.Invocations);
	}
}
