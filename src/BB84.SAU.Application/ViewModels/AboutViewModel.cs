using BB84.SAU.Application.ViewModels.Base;
using BB84.SAU.Domain.Models;

namespace BB84.SAU.Application.ViewModels;

/// <summary>
/// The about view model class.
/// </summary>
public sealed class AboutViewModel(AboutModel model) : ViewModelBase
{
	/// <summary>
	/// The model instance to use.
	/// </summary>
	public AboutModel Model => model;
}
