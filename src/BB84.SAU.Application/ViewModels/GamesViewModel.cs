using System.Windows.Controls;

using BB84.Extensions;
using BB84.Notifications.Attributes;
using BB84.Notifications.Commands;
using BB84.Notifications.Interfaces.Commands;
using BB84.SAU.Application.Interfaces.Application.Services;
using BB84.SAU.Application.ViewModels.Base;
using BB84.SAU.Domain.Models;
using BB84.SAU.Domain.Settings;

using Microsoft.Extensions.Options;

namespace BB84.SAU.Application.ViewModels;

/// <summary>
/// The games view model class.
/// </summary>
public sealed class GamesViewModel : ViewModelBase
{
	private readonly INavigationService _navigationService;
	private readonly ISteamWebService _steamWebService;
	private readonly SteamSettings _steamSettings;
	private readonly AchievementsViewModel _achievementsViewModel;
	private GameDetailModel? _selectedGame;
	private Image? _gameImage;
	private bool _gamesAreLoading;

	/// <summary>
	/// Initializes a new instance of the <see cref="GamesViewModel"/> class.
	/// </summary>
	/// <param name="navigationService">The navigation service instance to use.</param>
	/// <param name="steamWebService">The steam web service instance to use.</param>
	/// <param name="options">The steam setting options to use.</param>
	/// <param name="achievementsViewModel">The achievements view model instance to use.</param>
	/// <param name="userDataModel">The user data model instance to use.</param>
	public GamesViewModel(INavigationService navigationService, ISteamWebService steamWebService, IOptions<SteamSettings> options, AchievementsViewModel achievementsViewModel, UserDataModel userDataModel)
	{
		_navigationService = navigationService;
		_steamWebService = steamWebService;
		_steamSettings = options.Value;
		_achievementsViewModel = achievementsViewModel;

		Model = userDataModel;
		PropertyChanged += (s, e) => OnPropertyChanged(e.PropertyName);
	}

	/// <summary>
	/// The selected game to use.
	/// </summary>
	[NotifyChanged(nameof(IsGameVisible), nameof(IsSelectButtonVisible))]
	public GameDetailModel? SelectedGame
	{
		get => _selectedGame;
		set => SetProperty(ref _selectedGame, value);
	}

	/// <summary>
	/// The image of the selected game.
	/// </summary>
	public Image? GameImage
	{
		get => _gameImage;
		private set => SetProperty(ref _gameImage, value);
	}

	/// <summary>
	/// Indicates if the game group box should be visible.
	/// </summary>
	public bool IsGameVisible
		=> SelectedGame is not null;

	/// <summary>
	/// The user data model instance.
	/// </summary>
	public UserDataModel Model { get; }

	/// <summary>
	/// Indicates if the games are currently loading.
	/// </summary>
	public bool GamesAreLoading
	{
		get => _gamesAreLoading;
		private set => SetProperty(ref _gamesAreLoading, value);
	}

	/// <summary>
	/// The command to load the user games.
	/// </summary>
	public IAsyncActionCommand LoadGamesCommand
		=> new AsyncActionCommand(LoadGames, CanLoadGames);

	/// <summary>
	/// The command to select the game.
	/// </summary>
	public IActionCommand<GameDetailModel> SelectGameCommand
		=> new ActionCommand<GameDetailModel>(SelectGame, CanSelectGame);

	/// <summary>
	/// Indicates if the select game button is visible.
	/// </summary>
	public bool IsSelectButtonVisible
		=> SelectedGame is not null && SelectedGame.LastUpdate is not null;

	private bool CanLoadGames()
		=> Model.LastUpdate is not null && GamesAreLoading.IsFalse();

	private bool CanSelectGame(GameDetailModel model)
		=> model is not null && model.LastUpdate is not null;

	private void SelectGame(GameDetailModel model)
	{
		_achievementsViewModel.Model = model;
		_navigationService.NavigateTo<AchievementsViewModel>();
	}

	private async Task LoadGames()
	{
		try
		{
			GamesAreLoading = true;

			IEnumerable<GameModel> games = await _steamWebService.GetGames(_steamSettings.Id, _steamSettings.ApiKey)
				.ConfigureAwait(true);

			foreach (GameModel game in games)
			{
				GameDetailModel? gameDetail = await _steamWebService.GetGameDetails(game.Id)
					.ConfigureAwait(true);

				if (gameDetail is not null)
					Model.Games.Add(gameDetail);
			}
		}
		finally
		{
			GamesAreLoading = false;
		}
	}

	private void OnPropertyChanged(string? propertyName)
	{
		if (propertyName == nameof(SelectedGame) && SelectedGame is not null && SelectedGame.ImageUrl is not null)
			GameImage = CreateImageFromUri(new(SelectedGame.ImageUrl));
	}
}
