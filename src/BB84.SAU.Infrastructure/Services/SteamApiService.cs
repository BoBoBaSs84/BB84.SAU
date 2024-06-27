using BB84.SAU.Application.Interfaces.Application.Services;
using BB84.SAU.Application.Interfaces.Infrastructure.Services;
using BB84.SAU.Domain.Exceptions;
using BB84.SAU.Infrastructure.Interfaces.Provider;

using Microsoft.Extensions.Logging;

namespace BB84.SAU.Infrastructure.Services;

/// <summary>
/// The steam sdk api service class.
/// </summary>
/// <param name="loggerService">The logger service instance to use.</param>
/// <param name="notificationService">The notification service instance to use.</param>
/// <param name="steamWorksProvider">The steam works provider instance to use.</param>
/// <param name="fileProvider">The file provider instance to use.</param>
internal sealed class SteamApiService(ILoggerService<SteamApiService> loggerService, INotificationService notificationService, ISteamWorksProvider steamWorksProvider, IFileProvider fileProvider) : ISteamApiService
{
	private static readonly string SteamAppFileFullPath = Path.Combine(AppContext.BaseDirectory, "steam_appid.txt");

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
			fileProvider.WriteAllText(SteamAppFileFullPath, $"{appId}");

			bool success = steamWorksProvider.Init();

			if (success)
				AppId = appId;

			return Initialized;
		}
		catch (Exception ex)
		{
			loggerService.Log(LogExceptionWithParams, appId, ex);
			notificationService.Send(ex.Message);
			return false;
		}
	}

	public bool RequestCurrentStats()
	{
		try
		{
			if (!Initialized)
				throw new SteamSdkException("SDK is not initialized!");

			StatsRequested = steamWorksProvider.RequestCurrentStats();

			return StatsRequested;
		}
		catch (Exception ex)
		{
			loggerService.Log(LogException, ex);
			notificationService.Send(ex.Message);
			return false;
		}
	}

	public (bool Achieved, DateTime? UnlockTime) GetAchievement(string name)
	{
		try
		{
			if (!StatsRequested)
				throw new SteamSdkException("Stats have not been requested!");

			bool result = steamWorksProvider.GetAchievementAndUnlockTime(name, out bool achieved, out uint unlockTime);
			DateTime? dateTime = achieved
				? DateTimeOffset.FromUnixTimeSeconds(unlockTime).LocalDateTime
				: default;

			return (achieved, dateTime);
		}
		catch (Exception ex)
		{
			loggerService.Log(LogExceptionWithParams, name, ex);
			notificationService.Send(ex.Message);
			return (false, default);
		}
	}

	public bool ResetAchievement(string name)
	{
		try
		{
			if (!StatsRequested)
				throw new SteamSdkException("Stats have not been requested!");

			bool result = steamWorksProvider.ClearAchievement(name);

			return result;
		}
		catch (Exception ex)
		{
			loggerService.Log(LogExceptionWithParams, name, ex);
			notificationService.Send(ex.Message);
			return false;
		}
	}

	public bool UnlockAchievement(string name)
	{
		try
		{
			if (!StatsRequested)
				throw new SteamSdkException("Stats have not been requested!");

			bool result = steamWorksProvider.SetAchievement(name);

			return result;
		}
		catch (Exception ex)
		{
			loggerService.Log(LogExceptionWithParams, name, ex);
			notificationService.Send(ex.Message);
			return false;
		}
	}

	public void Shutdown()
	{
		try
		{
			if (!Initialized)
				throw new SteamSdkException("SDK is not initialized!");

			fileProvider.Delete(SteamAppFileFullPath);

			steamWorksProvider.Shutdown();

			AppId = null;
		}
		catch (Exception ex)
		{
			loggerService.Log(LogException, ex);
		}
	}

	public bool StoreStats()
	{
		try
		{
			return !StatsRequested
				? throw new SteamSdkException("Stats have not been requested!")
				: steamWorksProvider.StoreStats();
		}
		catch (Exception ex)
		{
			loggerService.Log(LogException, ex);
			notificationService.Send(ex.Message);
			return false;
		}
	}
}
