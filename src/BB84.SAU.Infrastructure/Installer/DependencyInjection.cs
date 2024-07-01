using System.Diagnostics.CodeAnalysis;

using BB84.SAU.Application.Interfaces.Infrastructure.Persistence;
using BB84.SAU.Application.Interfaces.Infrastructure.Services;
using BB84.SAU.Infrastructure.Extensions;
using BB84.SAU.Infrastructure.Interfaces.Provider;
using BB84.SAU.Infrastructure.Persistence;
using BB84.SAU.Infrastructure.Provider;
using BB84.SAU.Infrastructure.Services;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace BB84.SAU.Infrastructure.Installer;

/// <summary>
/// The infrastructure dependency injection class.
/// </summary>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, dependency injection.")]
public static class DependencyInjection
{
	/// <summary>
	/// Registers the infrastructure services to the service collection.
	/// </summary>
	/// <param name="services">The service collection to enrich.</param>
	/// <param name="environment">The host environment instance to use.</param>
	/// <returns>The enriched service collection.</returns>
	public static IServiceCollection RegisterInfrastructureServices(this IServiceCollection services, IHostEnvironment environment)
	{
		services.RegisterLoggerService(environment);

		services.TryAddSingleton<IFileProvider, FileProvider>();
		services.TryAddSingleton<ISteamWorksProvider, SteamWorksProvider>();
		services.TryAddSingleton<ISteamApiService, SteamApiService>();
		services.TryAddSingleton<IUserDataService, UserDataService>();

		return services;
	}
}
