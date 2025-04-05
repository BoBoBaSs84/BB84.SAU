// -----------------------------------------------------------------------------
// Copyright:	Robert Peter Meyer
// License:		MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------------
using System.Net;
using System.Text;

using BB84.SAU.Application.Interfaces.Application.Provider;
using BB84.SAU.Application.Interfaces.Application.Services;
using BB84.SAU.Application.Interfaces.Infrastructure.Services;
using BB84.SAU.Application.Services;

using Moq;
using Moq.Protected;

namespace BB84.SAU.Application.Tests.Services;

[TestClass]
public sealed partial class SteamWebServiceTests
{
	private const int APPID = 1;
	private const string APIKEY = "UnitTestKey";
	private const long STEAMID = 1;
	private const string AchievmentsResponse = @"{""game"":{""gameName"":""MyTestGame"",""gameVersion"":""1"",""availableGameStats"":{""achievements"":[{""name"":""Achievement"",""defaultvalue"":0,""displayName"":""Achievement"",""hidden"":0,""description"":""Achievement"",""icon"":""https://www.unittest.dev/1.jpg"",""icongray"":""https://www.unittest.dev/2.jpg""}]}}}";
	private const string GamesResponse = @"{""response"":{""games"":[{""appid"": 1,""name"":""UnitTest""}]}}";
	private const string GameDetailsResponse = @"{""1"":{""data"":{""name"":""UnitTest"",""steam_appid"":1,""short_description"":""UnitTest"",""header_image"":""https://www.unittest.dev/1.jpg""}}}";
	private const string UserDataResponse = @"{""response"":{""players"":[{""steamid"":""1"",""personaname"":""UnitTest"",""profileurl"":""http://unittest.dev/profiles/1/"",""avatarfull"":""http://unittest.dev/1.jpg"",""lastlogoff"":1,""timecreated"":1}]}}";

	private Mock<ILoggerService<SteamWebService>> _loggerServiceMock = new();
	private Mock<INotificationService> _notificationServiceMock = new();
	private Mock<IDateTimeProvider> _dateTimeProviderMock = new();
	private Mock<IHttpClientFactory> _httpClientFactoryMock = new();
	private Mock<HttpMessageHandler> _httpMessageHandler = new();

	private SteamWebService CreateMockedInstance()
	{
		_loggerServiceMock = new();
		_notificationServiceMock = new();
		_dateTimeProviderMock = new();
		_httpClientFactoryMock = new();

		return new(_loggerServiceMock.Object, _notificationServiceMock.Object, _dateTimeProviderMock.Object, _httpClientFactoryMock.Object);
	}

	private HttpClient CreateMockedClient(HttpStatusCode statusCode, string? content = null)
	{
		_httpMessageHandler = new(MockBehavior.Strict);
		HttpResponseMessage responseMessage = new(statusCode)
		{
			Content = new StringContent(content ?? string.Empty, Encoding.UTF8, "application/json")
		};
		_httpMessageHandler.Protected()
			.Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
			.ReturnsAsync(responseMessage)
			.Verifiable();
		return new(_httpMessageHandler.Object) { BaseAddress = new Uri("http://www.unittest.dev") };
	}
}
