using System.Net.Http;
using System.Text.Json;

using BB84.Extensions;

using Microsoft.Extensions.Logging;

using BB84.SAU.Application.Interfaces.Application.Provider;
using BB84.SAU.Application.Interfaces.Application.Services;
using BB84.SAU.Application.Interfaces.Infrastructure.Services;
using BB84.SAU.Domain.Models;

using HC = BB84.SAU.Application.Common.ApplicationConstants.HttpClients;

namespace BB84.SAU.Application.Services;

/// <summary>
/// The steam web service class.
/// </summary>
/// <param name="loggerService">The logger service instance to use.</param>
/// <param name="notificationService">The notification service instance to use.</param>
/// <param name="dateTimeProvider">The date time provider instance to use.</param>
/// <param name="httpClientFactory">The http client factory instance to use.</param>
internal sealed class SteamWebService(ILoggerService<SteamWebService> loggerService, INotificationService notificationService, IDateTimeProvider dateTimeProvider, IHttpClientFactory httpClientFactory) : ISteamWebService
{
	private readonly ILoggerService<SteamWebService> _loggerService = loggerService;
	private readonly INotificationService _notificationService = notificationService;
	private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

	private static readonly Action<ILogger, Exception?> LogException =
		LoggerMessage.Define(LogLevel.Error, 0, "Exception occured.");

	private static readonly Action<ILogger, object, Exception?> LogExceptionWithParams =
		LoggerMessage.Define<object>(LogLevel.Error, 0, "Exception occured. Params = {Parameters}");

	public async Task<IEnumerable<AchievementModel>> GetAchievements(int appId, string apiKey, CancellationToken cancellationToken = default)
	{
		List<AchievementModel> achievements = [];
		try
		{
			HttpClient httpClient = _httpClientFactory.CreateClient(nameof(HC.SteamPowered));
			string requestUri = HC.SteamPowered.GetSchemaForGame.FormatInvariant(apiKey, appId);
			using HttpResponseMessage responseMessage = await httpClient.GetAsync(requestUri, cancellationToken);

			if (!responseMessage.IsSuccessStatusCode)
			{
				_loggerService.Log(LogExceptionWithParams, responseMessage.StatusCode, null);
				return achievements;
			}

			string jsonContent = await responseMessage.Content.ReadAsStringAsync(cancellationToken)
				.ConfigureAwait(false);

			JsonElement.ArrayEnumerator jsonArray = JsonSerializer.Deserialize<JsonElement>(jsonContent)
				.GetProperty("game")
				.GetProperty("availableGameStats")
				.GetProperty("achievements")
				.EnumerateArray();

			foreach (JsonElement jsonElement in jsonArray)
			{
				string id = jsonElement.GetProperty("name").ToString();
				string name = jsonElement.GetProperty("displayName").ToString();
				bool hidden = jsonElement.GetProperty("hidden").GetInt32() == 1;

				string? description = null;
				if (jsonElement.TryGetProperty("description", out JsonElement value))
					description = value.GetString();

				string icon = jsonElement.GetProperty("icon").ToString();
				string iconGray = jsonElement.GetProperty("icongray").ToString();

				achievements.Add(new(id, name, description, hidden, icon, iconGray));
			}

			_notificationService.Send($"Loaded {achievements.Count} achievements.");

			return achievements;
		}
		catch (Exception ex)
		{
			_loggerService.Log(LogExceptionWithParams, appId, ex);
			_notificationService.Send(ex.Message);
			return achievements;
		}
	}

	public async Task<IEnumerable<GameModel>> GetGames(long steamId, string apiKey, CancellationToken cancellationToken = default)
	{
		List<GameModel> games = [];
		try
		{
			HttpClient httpClient = _httpClientFactory.CreateClient(nameof(HC.SteamPowered));
			string requestUri = HC.SteamPowered.GetOwnedGames.FormatInvariant(apiKey, steamId);
			using HttpResponseMessage gamesResponseMessage = await httpClient.GetAsync(requestUri, cancellationToken);

			if (!gamesResponseMessage.IsSuccessStatusCode)
			{
				_loggerService.Log(LogExceptionWithParams, gamesResponseMessage.StatusCode, null);
				return games;
			}

			string jsonContent = await gamesResponseMessage.Content.ReadAsStringAsync(cancellationToken)
				.ConfigureAwait(false);

			JsonElement.ArrayEnumerator jsonArray = JsonSerializer.Deserialize<JsonElement>(jsonContent)
				.GetProperty("response")
				.GetProperty("games")
				.EnumerateArray();

			foreach (var jsonElement in jsonArray)
			{
				int id = jsonElement.GetProperty("appid").GetInt32();
				string title = jsonElement.GetProperty("name").ToString();

				games.Add(new(id, title));
			}

			_notificationService.Send($"Loaded {games.Count} games.");

			return games;
		}
		catch (Exception ex)
		{
			_loggerService.Log(LogException, ex);
			_notificationService.Send(ex.Message);
			return games;
		}
	}

	public async Task<GameDetailModel?> GetGameDetails(int appId, CancellationToken cancellationToken = default)
	{
		try
		{
			HttpClient httpClient = _httpClientFactory.CreateClient(nameof(HC.SteamStore));
			string requestUri = HC.SteamStore.GetAppDetails.FormatInvariant(appId);
			using HttpResponseMessage responseMessage = await httpClient.GetAsync(requestUri, cancellationToken);

			if (!responseMessage.IsSuccessStatusCode)
			{
				_loggerService.Log(LogExceptionWithParams, responseMessage.StatusCode, null);
				return default;
			}

			string jsonContent = await responseMessage.Content.ReadAsStringAsync(cancellationToken)
				.ConfigureAwait(false);

			JsonElement jsonElement = JsonSerializer.Deserialize<JsonElement>(jsonContent)
				.GetProperty($"{appId}")
				.GetProperty("data");

			int id = jsonElement.GetProperty("steam_appid").GetInt32();
			string title = jsonElement.GetProperty("name").ToString();

			string? description = default;
			if (jsonElement.TryGetProperty("short_description", out JsonElement value))
				description = value.GetString();

			string? imageUrl = default;
			if (jsonElement.TryGetProperty("header_image", out value))
				imageUrl = value.GetString();

			return new(id, title, description, imageUrl, dateTimeProvider.Now);
		}
		catch (Exception ex)
		{
			_loggerService.Log(LogExceptionWithParams, appId, ex);
			_notificationService.Send(ex.Message);
			return default;
		}
	}

	public async Task<UserDataModel?> GetUserProfile(long steamId, string apiKey, CancellationToken cancellationToken = default)
	{
		try
		{
			HttpClient httpClient = _httpClientFactory.CreateClient(nameof(HC.SteamPowered));
			string requestUri = HC.SteamPowered.GetPlayerSummaries.FormatInvariant(apiKey, steamId);
			using HttpResponseMessage responseMessage = await httpClient.GetAsync(requestUri, cancellationToken);

			if (!responseMessage.IsSuccessStatusCode)
			{
				_loggerService.Log(LogExceptionWithParams, responseMessage.StatusCode, null);
				return default;
			}

			string jsonContent = await responseMessage.Content.ReadAsStringAsync(cancellationToken)
				.ConfigureAwait(false);

			JsonElement jsonElement = JsonSerializer.Deserialize<JsonElement>(jsonContent)
				.GetProperty("response")
				.GetProperty("players")[0];

			string name = jsonElement.GetProperty("personaname").ToString();
			string imageUrl = jsonElement.GetProperty("avatarfull").ToString();

			string? profileUrl = default;
			if (jsonElement.TryGetProperty("profileurl", out JsonElement value))
				profileUrl = value.GetString();

			DateTime created = DateTimeOffset.FromUnixTimeMilliseconds(jsonElement.GetProperty("timecreated").GetInt64()).LocalDateTime;
			DateTime lastLogOff = DateTimeOffset.FromUnixTimeMilliseconds(jsonElement.GetProperty("lastlogoff").GetInt64()).LocalDateTime;

			return new(name, imageUrl, profileUrl, created, lastLogOff, dateTimeProvider.Now);
		}
		catch (Exception ex)
		{
			_loggerService.Log(LogException, ex);
			_notificationService.Send(ex.Message);
			return default;
		}
	}
}
