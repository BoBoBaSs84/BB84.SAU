using Microsoft.Extensions.Options;

using BB84.SAU.Application.Interfaces.Application.Services;
using BB84.SAU.Application.ViewModels.Base;
using BB84.SAU.Domain.Settings;

namespace BB84.SAU.Application.ViewModels;

/// <summary>
/// The settings view model class.
/// </summary>
/// <param name="options">The steam setting options to use.</param>
/// <param name="navigationService">The navigation service instance to use.</param>
public sealed class SettingsViewModel(IOptions<SteamSettings> options, INavigationService navigationService) : ViewModelBase
{
	/// <summary>
	/// The steam settings to use.
	/// </summary>
	public SteamSettings Model { get; } = options.Value;

	/// <summary>
	/// The navigation service to use.
	/// </summary>
	public INavigationService NavigationService { get; } = navigationService;
}
