using System.Windows;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using BB84.SAU.Application.Interfaces.Infrastructure.Services;
using BB84.SAU.Extensions;
using BB84.SAU.Presentation.Windows;

using WinApplication = System.Windows.Application;

namespace BB84.SAU;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : WinApplication
{
	private readonly IHost _host;
	private readonly ILoggerService<App> _loggerService;
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
		_steamApiService = _host.Services.GetRequiredService<ISteamApiService>();

		DispatcherUnhandledException += (s, e) => OnUnhandledException(e.Exception);
	}

	private async void Application_Startup(object sender, StartupEventArgs e)
	{
		_loggerService.Log(LogInformation, "Application starting...");

		await _host.StartAsync().ConfigureAwait(false);

		MainWindow mainWindow = _host.Services.GetRequiredService<MainWindow>();
		mainWindow.Show();
	}

	private async void Application_Exit(object sender, ExitEventArgs e)
	{
		_loggerService.Log(LogInformation, "Application exiting...");

		if (_steamApiService.AppId is not null)
			_steamApiService.Shutdown();

		using (_host)
			await _host.StopAsync(TimeSpan.FromSeconds(5)).ConfigureAwait(false);
	}

	private void OnUnhandledException(Exception exception)
		=> _loggerService.Log(LogCritical, exception);

	private static IHostBuilder CreateHostBuilder()
		=> Host.CreateDefaultBuilder().AddApplicationSettings().ConfigureServices((context, services)
				=> services.RegisterServices(context.HostingEnvironment, context.Configuration));
}
