using BB84.SAU.Application.ViewModels.Base;

namespace BB84.SAU.Application.Interfaces.Application.Services;

/// <summary>
/// The navgigation service interface.
/// </summary>
public interface INavigationService
{
	/// <summary>
	/// The current view model.
	/// </summary>
	ViewModelBase CurrentView { get; }

	/// <summary>
	/// Navigates to the provided view model.
	/// </summary>
	/// <typeparam name="T">The view model to navigate to.</typeparam>
	void NavigateTo<T>() where T : ViewModelBase;
}
