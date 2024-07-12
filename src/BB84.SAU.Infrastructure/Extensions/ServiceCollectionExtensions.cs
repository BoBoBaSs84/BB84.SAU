using System.Diagnostics.CodeAnalysis;

using BB84.SAU.Application.Interfaces.Infrastructure.Persistence;
using BB84.SAU.Application.Interfaces.Infrastructure.Services;
using BB84.SAU.Infrastructure.Interfaces.Provider;
using BB84.SAU.Infrastructure.Persistence;
using BB84.SAU.Infrastructure.Provider;
using BB84.SAU.Infrastructure.Services;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BB84.SAU.Infrastructure.Extensions;

/// <summary>
/// The infrastructure service collection extensions.
/// </summary>
[ExcludeFromCodeCoverage(Justification = "Dependency injection helper.")]
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, dependency injection.")]
internal static class ServiceCollectionExtensions
{
	/// <summary>
	/// Registers the logger service to the service collection.
	/// </summary>
	/// <param name="services">The service collection to enrich.</param>
	/// <param name="environment">The host environment to use.</param>
	/// <returns>The enriched service collection.</returns>
	internal static IServiceCollection RegisterLoggerService(this IServiceCollection services, IHostEnvironment environment)
	{
		services.TryAddSingleton(typeof(ILoggerService<>), typeof(LoggerService<>));

		services.AddLogging(config =>
		{
			config.ClearProviders();
			config.AddEventLog(config => config.SourceName = environment.ApplicationName);

			if (environment.IsDevelopment())
				config.SetMinimumLevel(LogLevel.Debug);

			if (environment.IsProduction())
				config.SetMinimumLevel(LogLevel.Warning);
		});

		return services;
	}

	/// <summary>
	/// Registers the infrastructure providers to the service collection.
	/// </summary>
	/// <param name="services">The service collection to enrich.</param>
	/// <returns>The enriched service collection.</returns>
	internal static IServiceCollection RegisterProviders(this IServiceCollection services)
	{
		services.TryAddSingleton<IDirectoryProvider, DirectoryProvider>();
		services.TryAddSingleton<IFileProvider, FileProvider>();
		services.TryAddSingleton<ISteamWorksProvider, SteamWorksProvider>();

		return services;
	}

	/// <summary>
	/// Registers the infrastructure services to the service collection.
	/// </summary>
	/// <param name="services">The service collection to enrich.</param>
	/// <returns>The enriched service collection.</returns>
	internal static IServiceCollection RegisterServices(this IServiceCollection services)
	{
		services.TryAddSingleton<ISteamApiService, SteamApiService>();
		services.TryAddSingleton<IUserDataService, UserDataService>();

		return services;
	}
}
