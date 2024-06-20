using BB84.SAU.Application.Interfaces.Application.Services;
using BB84.SAU.Application.ViewModels.Base;

namespace BB84.SAU.Application.ViewModels;

/// <summary>
/// The main view model class.
/// </summary>
/// <param name="navigationService">The navigation service instance to use.</param>
public sealed class MainViewModel(INavigationService navigationService) : ViewModelBase
{
	/// <summary>
	/// The navigation service instance.
	/// </summary>
	public INavigationService NavigationService { get; } = navigationService;
}
