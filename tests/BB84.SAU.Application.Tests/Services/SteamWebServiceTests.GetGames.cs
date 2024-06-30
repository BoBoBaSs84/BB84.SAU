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
	public async Task GetGamesShouldReturnEmptyResultWhenExceptionGetsThrown()
	{
		SteamWebService service = CreateMockedInstance();
		_httpClientFactoryMock.Setup(x => x.CreateClient(It.IsAny<string>())).Throws<HttpRequestException>();

		IEnumerable<GameModel> result = await service.GetGamesAsync(STEAMID, APIKEY)
			.ConfigureAwait(false);

		Assert.IsFalse(result.Any());
		Assert.AreEqual(1, _loggerServiceMock.Invocations.Count);
		Assert.AreEqual(1, _notificationServiceMock.Invocations.Count);
	}

	[TestMethod]
	[TestCategory("Methods")]
	public async Task GetGamesShouldReturnEmptyResultWhenNotSuccessful()
	{
		SteamWebService service = CreateMockedInstance();
		_httpClientFactoryMock.Setup(x => x.CreateClient(It.IsAny<string>()))
			.Returns(CreateMockedClient(HttpStatusCode.NotFound));

		IEnumerable<GameModel> result = await service.GetGamesAsync(STEAMID, APIKEY)
			.ConfigureAwait(false);

		Assert.IsFalse(result.Any());
		Assert.AreEqual(1, _httpClientFactoryMock.Invocations.Count);
		Assert.AreEqual(1, _httpMessageHandler.Invocations.Count);
		Assert.AreEqual(1, _loggerServiceMock.Invocations.Count);
		Assert.AreEqual(1, _notificationServiceMock.Invocations.Count);
	}

	[TestMethod]
	[TestCategory("Methods")]
	public async Task GetGamesShouldReturnOneResultWhenSuccessful()
	{
		SteamWebService service = CreateMockedInstance();
		_httpClientFactoryMock.Setup(x => x.CreateClient(It.IsAny<string>()))
			.Returns(CreateMockedClient(HttpStatusCode.OK, GamesResponse));

		IEnumerable<GameModel> result = await service.GetGamesAsync(STEAMID, APIKEY)
			.ConfigureAwait(false);

		Assert.IsTrue(result.Any());
		Assert.AreEqual(1, _httpClientFactoryMock.Invocations.Count);
		Assert.AreEqual(1, _httpMessageHandler.Invocations.Count);
		Assert.AreEqual(0, _loggerServiceMock.Invocations.Count);
		Assert.AreEqual(1, _notificationServiceMock.Invocations.Count);
	}
}
