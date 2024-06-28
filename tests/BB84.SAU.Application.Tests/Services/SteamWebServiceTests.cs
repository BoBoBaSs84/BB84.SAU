using System.Net;
using System.Text;

using BB84.SAU.Application.Interfaces.Application.Provider;
using BB84.SAU.Application.Interfaces.Application.Services;
using BB84.SAU.Application.Interfaces.Infrastructure.Services;
using BB84.SAU.Application.Services;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;
using Moq.Protected;

namespace BB84.SAU.Application.Tests.Services;

[TestClass]
public sealed partial class SteamWebServiceTests
{	
	private const int APPID = 1;
	private const string APIKEY = "UnitTestKey";
	private const string AchievmentsResponse = @"{""game"":{""gameName"":""MyTestGame"",""gameVersion"":""1"",""availableGameStats"":{""achievements"":[{""name"":""Achievement"",""defaultvalue"":0,""displayName"":""Achievement"",""hidden"":0,""description"":""Achievement"",""icon"":""https://www.unittest.dev/1.jpg"",""icongray"":""https://www.unittest.dev/2.jpg""}]}}}";
	
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
