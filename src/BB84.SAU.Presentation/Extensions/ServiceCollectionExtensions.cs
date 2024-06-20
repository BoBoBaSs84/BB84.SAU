using BB84.SAU.Presentation.Controls;
using BB84.SAU.Presentation.Windows;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace BB84.SAU.Presentation.Extensions;

/// <summary>
/// The presentation service collection extensions.
/// </summary>
internal static class ServiceCollectionExtensions
{
	/// <summary>
	/// Registers the required controls to the service collection.
	/// </summary>
	/// <param name="services">The service collection to enrich.</param>
	/// <returns>The enriched service collection.</returns>
	internal static IServiceCollection RegisterControls(this IServiceCollection services)
	{
		services.TryAddSingleton<AboutControl>();
		services.TryAddSingleton<UserDataControl>();

		return services;
	}

	/// <summary>
	/// Registers the required windows to the service collection.
	/// </summary>
	/// <param name="services">The service collection to enrich.</param>
	/// <returns>The enriched service collection.</returns>
	internal static IServiceCollection RegisterWindows(this IServiceCollection services)
	{
		services.TryAddSingleton<MainWindow>();

		return services;
	}
}
