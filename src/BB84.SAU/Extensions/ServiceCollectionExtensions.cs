using System.Diagnostics.CodeAnalysis;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using BB84.SAU.Application.Installer;
using BB84.SAU.Domain.Installer;
using BB84.SAU.Domain.Settings;
using BB84.SAU.Infrastructure.Installer;
using BB84.SAU.Presentation.Installer;

namespace BB84.SAU.Extensions;

/// <summary>
/// The service collection extensions class.
/// </summary>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here.")]
internal static class ServiceCollectionExtensions
{
	/// <summary>
	/// Registers the required services to the service collection.
	/// </summary>
	/// <param name="services">The service collection to enrich.</param>
	/// <param name="environment">The host environment instance to use.</param>
	/// <param name="configuration">The configuration instance to use.</param>
	/// <returns>The enriched service collection.</returns>
	internal static IServiceCollection RegisterServices(this IServiceCollection services, IHostEnvironment environment, IConfiguration configuration)
	{
		services.RegisterApplicationServices();
		services.RegisterDomainServices();
		services.RegisterInfrastructureServices(environment);
		services.RegisterPresentationServices();

		services.RegisterSteamSettings(configuration);

		return services;
	}

	/// <summary>
	/// Registers the steam setting options to the service collection.
	/// </summary>
	/// <param name="services">The service collection to enrich.</param>
	/// <param name="configuration">The configuration instance to use.</param>
	/// <returns>The enriched service collection.</returns>
	private static IServiceCollection RegisterSteamSettings(this IServiceCollection services, IConfiguration configuration)
	{
		services.Configure<SteamSettings>(configuration.GetSection(nameof(SteamSettings)))
			.AddOptions();

		return services;
	}
}
