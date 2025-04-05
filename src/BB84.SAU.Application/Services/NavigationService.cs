// -----------------------------------------------------------------------------
// Copyright:	Robert Peter Meyer
// License:		MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------------
using BB84.Notifications;

using BB84.SAU.Application.Interfaces.Application.Services;
using BB84.SAU.Application.ViewModels.Base;

namespace BB84.SAU.Application.Services;

/// <summary>
/// The navigation service class.
/// </summary>
/// <param name="viewModelFactory">The view model factory to use.</param>
internal sealed class NavigationService(Func<Type, ViewModelBase> viewModelFactory) : NotifiableObject, INavigationService
{
	private ViewModelBase _currentView = default!;

	public ViewModelBase CurrentView
	{
		get => _currentView;
		private set => SetProperty(ref _currentView, value);
	}

	public void NavigateTo<T>() where T : ViewModelBase
		=> CurrentView = viewModelFactory.Invoke(typeof(T));
}
