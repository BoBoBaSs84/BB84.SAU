﻿// -----------------------------------------------------------------------------
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
	public async Task GetGameDetailsShouldReturnNullWhenExceptionGetsThrown()
	{
		SteamWebService service = CreateMockedInstance();
		_httpClientFactoryMock.Setup(x => x.CreateClient(It.IsAny<string>())).Throws<HttpRequestException>();

		GameDetailModel? result = await service.GetGameDetailsAsync(APPID)
			.ConfigureAwait(false);

		Assert.IsNull(result);
		Assert.AreEqual(1, _loggerServiceMock.Invocations.Count);
		Assert.AreEqual(1, _notificationServiceMock.Invocations.Count);
	}

	[TestMethod]
	[TestCategory("Methods")]
	public async Task GetGameDetailsShouldReturnNullWhenNotSuccessful()
	{
		SteamWebService service = CreateMockedInstance();
		_httpClientFactoryMock.Setup(x => x.CreateClient(It.IsAny<string>()))
			.Returns(CreateMockedClient(HttpStatusCode.NotFound));

		GameDetailModel? result = await service.GetGameDetailsAsync(APPID)
			.ConfigureAwait(false);

		Assert.IsNull(result);
		Assert.AreEqual(1, _httpClientFactoryMock.Invocations.Count);
		Assert.AreEqual(1, _httpMessageHandler.Invocations.Count);
		Assert.AreEqual(1, _loggerServiceMock.Invocations.Count);
		Assert.AreEqual(1, _notificationServiceMock.Invocations.Count);
	}

	[TestMethod]
	[TestCategory("Methods")]
	public async Task GetGameDetailsShouldReturnResultWhenSuccessful()
	{
		SteamWebService service = CreateMockedInstance();
		_httpClientFactoryMock.Setup(x => x.CreateClient(It.IsAny<string>()))
			.Returns(CreateMockedClient(HttpStatusCode.OK, GameDetailsResponse));

		GameDetailModel? result = await service.GetGameDetailsAsync(APPID)
			.ConfigureAwait(false);

		Assert.IsNotNull(result);
		Assert.AreEqual(1, _httpClientFactoryMock.Invocations.Count);
		Assert.AreEqual(1, _httpMessageHandler.Invocations.Count);
		Assert.AreEqual(0, _loggerServiceMock.Invocations.Count);
		Assert.AreEqual(1, _notificationServiceMock.Invocations.Count);
	}
}
