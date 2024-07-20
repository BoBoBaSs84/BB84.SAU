using System.Windows;

using BB84.SAU.Application.Interfaces.Application.Services;
using BB84.SAU.Application.Interfaces.Infrastructure.Persistence;
using BB84.SAU.Application.Interfaces.Infrastructure.Services;
using BB84.SAU.Domain.Models;
using BB84.SAU.Extensions;
using BB84.SAU.Presentation.Windows;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using WinApplication = System.Windows.Application;

namespace BB84.SAU;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : WinApplication
{
	private readonly IHost _host;
	private readonly ILoggerService<App> _loggerService;
	private readonly INotificationService _notificationService;
	private readonly ISteamApiService _steamApiService;

	private static readonly Action<ILogger, string, Exception?> LogInformation =
		LoggerMessage.Define<string>(LogLevel.Information, 0, "{Information}");

	private static readonly Action<ILogger, Exception?> LogCritical =
		LoggerMessage.Define(LogLevel.Critical, 0, string.Empty);

	/// <summary>
	/// Initializes a new instance of the app class.
	/// </summary>
	public App()
	{
		_host = CreateHostBuilder().Build();

		_loggerService = _host.Services.GetRequiredService<ILoggerService<App>>();
		_notificationService = _host.Services.GetRequiredService<INotificationService>();
		_steamApiService = _host.Services.GetRequiredService<ISteamApiService>();

		DispatcherUnhandledException += (s, e) => OnUnhandledException(e.Exception);
	}

	private async void Application_Startup(object sender, StartupEventArgs e)
	{
		string message = $"Application starting...";
		_notificationService.Send(message);
		_loggerService.Log(LogInformation, message);

		await _host.StartAsync().ConfigureAwait(false);
		await LoadUserDataAsync();

		MainWindow mainWindow = _host.Services.GetRequiredService<MainWindow>();
		mainWindow.Show();
	}

	private async void Application_Exit(object sender, ExitEventArgs e)
	{
		string message = $"Application exiting...";
		_notificationService.Send(message);
		_loggerService.Log(LogInformation, message);

		if (_steamApiService.AppId is not null)
			_steamApiService.Shutdown();

		await SaveUserDataAsync();

		using (_host)
			await _host.StopAsync(TimeSpan.FromSeconds(5)).ConfigureAwait(false);
	}

	private void OnUnhandledException(Exception exception)
		=> _loggerService.Log(LogCritical, exception);

	private static IHostBuilder CreateHostBuilder()
		=> Host.CreateDefaultBuilder().AddApplicationSettings().ConfigureServices((context, services)
				=> services.RegisterServices(context.HostingEnvironment, context.Configuration));

	private async Task LoadUserDataAsync()
	{
		IUserDataService userDataService = _host.Services.GetRequiredService<IUserDataService>();
		UserDataModel currentUserData = _host.Services.GetRequiredService<UserDataModel>();

		UserDataModel loadedUserData = await userDataService.LoadUserDataAsync()
			.ConfigureAwait(false);

		currentUserData.Created = loadedUserData.Created;
		currentUserData.Name = loadedUserData.Name;
		currentUserData.ProfileUrl = loadedUserData.ProfileUrl;
		currentUserData.ImageUrl = loadedUserData.ImageUrl;
		currentUserData.LastLogOff = loadedUserData.LastLogOff;
		currentUserData.LastUpdate = loadedUserData.LastUpdate;
		currentUserData.Games = loadedUserData.Games;

		string message = $"User data loaded.";
		_notificationService.Send(message);
		_loggerService.Log(LogInformation, message);
	}

	private async Task SaveUserDataAsync()
	{
		IUserDataService userDataService = _host.Services.GetRequiredService<IUserDataService>();
		UserDataModel currentUserData = _host.Services.GetRequiredService<UserDataModel>();

		_ = await userDataService.SaveUserDataAsync(currentUserData)
			.ConfigureAwait(false);

		string message = $"User data saved.";
		_notificationService.Send(message);
		_loggerService.Log(LogInformation, message);
	}
}
