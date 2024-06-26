using System.Diagnostics.CodeAnalysis;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using BB84.SAU.Application.Common;
using BB84.SAU.Application.Interfaces.Application.Provider;
using BB84.SAU.Application.Interfaces.Application.Services;
using BB84.SAU.Application.Provider;
using BB84.SAU.Application.Services;
using BB84.SAU.Application.ViewModels;
using BB84.SAU.Application.ViewModels.Base;

namespace BB84.SAU.Application.Extensions;

/// <summary>
/// The service collection extensions class.
/// </summary>
[ExcludeFromCodeCoverage(Justification = "Dependency injection helper.")]
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, dependency injection.")]
internal static class ServiceCollectionExtensions
{
	/// <summary>
	/// Registers the required view models to the service collection.
	/// </summary>
	/// <param name="services">The service collection to enrich.</param>
	/// <returns>The enriched service collection.</returns>
	internal static IServiceCollection RegisterViewModels(this IServiceCollection services)
	{
		services.TryAddSingleton<AboutViewModel>();
		services.TryAddSingleton<AchievementsViewModel>();
		services.TryAddSingleton<GamesViewModel>();
		services.TryAddSingleton<LogbookViewModel>();
		services.TryAddSingleton<MainViewModel>();
		services.TryAddSingleton<SettingsViewModel>();
		services.TryAddSingleton<UserDataViewModel>();

		return services;
	}

	/// <summary>
	/// Registers the named http clients to the service collection.
	/// </summary>
	/// <param name="services">The service collection to enrich.</param>
	/// <returns>The enriched service collection.</returns>
	internal static IServiceCollection RegisterHttpClients(this IServiceCollection services)
	{
		services.AddHttpClient(nameof(ApplicationConstants.HttpClients.SteamPowered), client =>
		{
			client.BaseAddress = new Uri(ApplicationConstants.HttpClients.SteamPowered.BaseUri);
			client.Timeout = new(0, 0, 10);
			client.DefaultRequestHeaders.Clear();
		});

		services.AddHttpClient(nameof(ApplicationConstants.HttpClients.SteamStore), client =>
		{
			client.BaseAddress = new Uri(ApplicationConstants.HttpClients.SteamStore.BaseUri);
			client.Timeout = new(0, 0, 10);
			client.DefaultRequestHeaders.Clear();
		});

		return services;
	}

	/// <summary>
	/// Registers the required application providers to the service collection.
	/// </summary>
	/// <param name="services">The service collection to enrich.</param>
	/// <returns>The enriched service collection.</returns>
	internal static IServiceCollection RegisterProviders(this IServiceCollection services)
	{
		services.TryAddSingleton<IDateTimeProvider, DateTimeProvider>();

		return services;
	}

	/// <summary>
	/// Registers the required application services to the service collection.
	/// </summary>
	/// <param name="services">The service collection to enrich.</param>
	/// <returns>The enriched service collection.</returns>
	internal static IServiceCollection RegisterServices(this IServiceCollection services)
	{
		services.TryAddSingleton<ISteamWebService, SteamWebService>();
		services.TryAddSingleton<INavigationService, NavigationService>();
		services.TryAddSingleton<INotificationService, NotificationService>();

		services.TryAddSingleton<Func<Type, ViewModelBase>>(serviceProvider
			=> viewModelType => (ViewModelBase)serviceProvider.GetRequiredService(viewModelType));

		return services;
	}
}
