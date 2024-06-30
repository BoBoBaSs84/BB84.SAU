using System.Windows.Controls;

using BB84.Extensions;
using BB84.Notifications.Commands;
using BB84.Notifications.Interfaces.Commands;

using Microsoft.Extensions.Options;

using BB84.SAU.Application.Interfaces.Application.Services;
using BB84.SAU.Application.ViewModels.Base;
using BB84.SAU.Domain.Models;
using BB84.SAU.Domain.Settings;

namespace BB84.SAU.Application.ViewModels;

/// <summary>
/// The user data view model class.
/// </summary>
public sealed class UserDataViewModel : ViewModelBase
{
	private readonly ISteamWebService _steamWebService;
	private readonly SteamSettings _steamSettings;
	private Image? _image;
	private bool _isUserDataLoading;

	/// <summary>
	/// Initializes a new instance of the <see cref="UserDataViewModel"/> class.
	/// </summary>
	/// <param name="steamWebService">The steam web service instance to use.</param>
	/// <param name="options">The steam setting options to use.</param>
	/// <param name="model">The user data model instance to use.</param>
	public UserDataViewModel(ISteamWebService steamWebService, IOptions<SteamSettings> options, UserDataModel model)
	{
		_steamWebService = steamWebService;
		_steamSettings = options.Value;

		Model = model;
		Model.PropertyChanged += (s, e) => OnModelPropertyChanged(e.PropertyName);
	}

	/// <summary>
	/// The user image.
	/// </summary>
	public Image? Image
	{
		get => _image;
		private set => SetProperty(ref _image, value);
	}

	/// <summary>
	/// The user data model instance.
	/// </summary>
	public UserDataModel Model { get; }

	/// <summary>
	/// Indicates if the user data is loading.
	/// </summary>
	public bool IsUserDataLoading
	{
		get => _isUserDataLoading;
		private set => SetProperty(ref _isUserDataLoading, value);
	}

	/// <summary>
	/// Indicates if the user data grid should be visible.
	/// </summary>
	public bool IsUserDataGridVisible
		=> Model.LastUpdate is not null;

	/// <summary>
	/// Indicates if the user data load grid should be visible.
	/// </summary>
	public bool IsUserDataLoadGridVisible
		=> Model.LastUpdate is null;

	/// <summary>
	/// The command to load the user data.
	/// </summary>
	public IAsyncActionCommand LoadUserDataCommand
		=> new AsyncActionCommand(LoadUserDataAsync, CanLoadUserData);

	private bool CanLoadUserData()
		=> Model.LastUpdate is null && IsUserDataLoading.IsFalse();

	private async Task LoadUserDataAsync()
	{
		try
		{
			IsUserDataLoading = true;

			UserDataModel? userData = await _steamWebService.GetUserDataAsync(_steamSettings.Id, _steamSettings.ApiKey)
				.ConfigureAwait(true);

			if (userData is null)
				return;

			Model.Name = userData.Name;
			Model.ImageUrl = userData.ImageUrl;
			Model.ProfileUrl = userData.ProfileUrl;
			Model.Created = userData.Created;
			Model.LastLogOff = userData.LastLogOff;
			Model.LastUpdate = userData.LastUpdate;

			Image = CreateImageFromUri(new(Model.ImageUrl));
		}
		finally
		{
			IsUserDataLoading = false;
		}
	}

	private void OnModelPropertyChanged(string? propertyName)
	{
		if (propertyName is not null)
		{
			if (propertyName == nameof(Model.LastUpdate))
			{
				RaisePropertyChanged(nameof(IsUserDataGridVisible));
				RaisePropertyChanged(nameof(IsUserDataLoadGridVisible));
			}
		}
	}
}
