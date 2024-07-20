using BB84.Extensions;
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

	public bool Initialize(int appId)
	{
		try
		{
			fileProvider.WriteAllText(SteamAppFileFullPath, $"{appId}");

			bool success = steamWorksProvider.Init();

			if (success)
				AppId = appId;

			notificationService.Send($"SDK for AppId: '{appId}' initialized successfully.");

			return Initialized;
		}
		catch (Exception ex)
		{
			loggerService.Log(LogExceptionWithParams, appId, ex);
			notificationService.Send(ex.Message);
			return false;
		}
	}

	public bool RequestStats()
	{
		try
		{
			if (!Initialized)
				throw new SteamSdkException("SDK is not initialized!");

			StatsRequested = steamWorksProvider.RequestCurrentStats();

			notificationService.Send($"Current user stats requested successfully.");

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
				: null;

			if (result)
				notificationService.Send($"Achievement '{name}' requested successfully.");

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

			if (result)
				notificationService.Send($"Achievement '{name}' reseted successfully.");

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

			if (result)
				notificationService.Send($"Achievement '{name}' unlocked successfully.");

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
			if (StatsRequested.IsFalse())
				throw new SteamSdkException("Stats have not been requested!");

			bool result = steamWorksProvider.StoreStats();

			if (result)
				notificationService.Send($"Current user stats stored successfully.");

			return result;
		}
		catch (Exception ex)
		{
			loggerService.Log(LogException, ex);
			notificationService.Send(ex.Message);
			return false;
		}
	}
}
