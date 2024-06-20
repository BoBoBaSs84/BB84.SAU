using Microsoft.Extensions.Logging;

using BB84.SAU.Application.Interfaces.Application.Services;
using BB84.SAU.Application.Interfaces.Infrastructure.Services;
using BB84.SAU.Domain.Exceptions;

using Steamworks;

namespace BB84.SAU.Infrastructure.Services;

/// <summary>
/// The steam sdk api service class.
/// </summary>
/// <param name="loggerService">The logger service instance to use.</param>
/// <param name="notificationService">The notification service instance to use.</param>
internal sealed class SteamApiService(ILoggerService<SteamApiService> loggerService, INotificationService notificationService) : ISteamApiService
{
	private const string SteamAppFile = "steam_appid.txt";
	private static readonly string BaseDirectory = AppContext.BaseDirectory;
	private readonly ILoggerService<SteamApiService> _loggerService = loggerService;
	private readonly INotificationService _notificationService = notificationService;

	private static readonly Action<ILogger, Exception?> LogException =
		LoggerMessage.Define(LogLevel.Error, 0, "Exception occured.");

	private static readonly Action<ILogger, object, Exception?> LogExceptionWithParams =
		LoggerMessage.Define<object>(LogLevel.Error, 0, "Exception occured. Params = {Parameters}");

	public int? AppId { get; private set; }
	public bool Initialized => AppId is not null;
	public bool StatsRequested { get; private set; }

	public bool Init(int appId)
	{
		try
		{
			string filePath = Path.Combine(BaseDirectory, SteamAppFile);

			File.WriteAllText(filePath, $"{appId}");

			bool success = SteamAPI.Init();

			if (success)
				AppId = appId;

			return Initialized;
		}
		catch (Exception ex)
		{
			_loggerService.Log(LogExceptionWithParams, appId, ex);
			_notificationService.Send(ex.Message);
			return false;
		}
	}

	public bool RequestCurrentStats()
	{
		try
		{
			if (!Initialized)
				throw new SteamSdkException("SDK is not initialized!");

			StatsRequested = SteamUserStats.RequestCurrentStats();

			return StatsRequested;
		}
		catch (Exception ex)
		{
			_loggerService.Log(LogException, ex);
			_notificationService.Send(ex.Message);
			return false;
		}
	}

	public (bool Achieved, DateTime? UnlockTime) GetAchievement(string name)
	{
		try
		{
			if (!StatsRequested)
				throw new SteamSdkException("Stats have not been requested!");

			bool result = SteamUserStats.GetAchievementAndUnlockTime(name, out bool achieved, out uint unlockTime);
			DateTime? dateTime = achieved
				? DateTimeOffset.FromUnixTimeSeconds(unlockTime).LocalDateTime
				: default;

			return (achieved, dateTime);
		}
		catch (Exception ex)
		{
			_loggerService.Log(LogExceptionWithParams, name, ex);
			_notificationService.Send(ex.Message);
			return (false, default);
		}
	}

	public bool ResetAchievement(string name)
	{
		try
		{
			if (!StatsRequested)
				throw new SteamSdkException("Stats have not been requested!");

			bool result = SteamUserStats.ClearAchievement(name);

			return result;
		}
		catch (Exception ex)
		{
			_loggerService.Log(LogExceptionWithParams, name, ex);
			_notificationService.Send(ex.Message);
			return false;
		}
	}

	public bool UnlockAchievement(string name)
	{
		try
		{
			if (!StatsRequested)
				throw new SteamSdkException("Stats have not been requested!");

			bool result = SteamUserStats.SetAchievement(name);

			return result;
		}
		catch (Exception ex)
		{
			_loggerService.Log(LogExceptionWithParams, name, ex);
			_notificationService.Send(ex.Message);
			return false;
		}
	}

	public void Shutdown()
	{
		try
		{
			if (!Initialized)
				throw new SteamSdkException("SDK is not initialized!");

			string filePath = Path.Combine(BaseDirectory, SteamAppFile);

			File.Delete(filePath);

			SteamAPI.Shutdown();

			AppId = null;
		}
		catch (Exception ex)
		{
			_loggerService.Log(LogException, ex);
		}
	}

	public bool StoreStats()
	{
		try
		{
			return !StatsRequested
				? throw new SteamSdkException("Stats have not been requested!")
				: SteamUserStats.StoreStats();
		}
		catch (Exception ex)
		{
			_loggerService.Log(LogException, ex);
			_notificationService.Send(ex.Message);
			return false;
		}
	}
}
