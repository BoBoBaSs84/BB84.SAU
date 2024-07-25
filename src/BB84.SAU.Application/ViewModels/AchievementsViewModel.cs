using System.Windows.Controls;

using BB84.Extensions;
using BB84.Notifications.Attributes;
using BB84.Notifications.Commands;
using BB84.Notifications.Interfaces.Commands;
using BB84.SAU.Application.Interfaces.Application.Provider;
using BB84.SAU.Application.Interfaces.Application.Services;
using BB84.SAU.Application.Interfaces.Infrastructure.Services;
using BB84.SAU.Application.ViewModels.Base;
using BB84.SAU.Domain.Models;
using BB84.SAU.Domain.Settings;

using Microsoft.Extensions.Options;

namespace BB84.SAU.Application.ViewModels;

/// <summary>
/// The achievements view model class.
/// </summary>
public sealed class AchievementsViewModel : ViewModelBase
{
	private readonly ISteamApiService _steamApiService;
	private readonly ISteamWebService _steamWebService;
	private readonly SteamSettings _steamSettings;
	private readonly IDateTimeProvider _dateTimeProvider;
	private AchievementModel? _selectedAchievement;
	private Image? _achievementImage;
	private GameDetailModel _selectedGame;
	private bool _hasAchievements;
	private bool _isAchievementsLoading;
	private float _overallAchievementProgress;

	/// <summary>
	/// Initializes a new instance of the <see cref="AchievementsViewModel"/> class.
	/// </summary>
	/// <param name="steamApiService">The steam api service instance to use.</param>
	/// <param name="steamWebService">The steam web service instance to use.</param>
	/// <param name="options">The steam setting options to use.</param>
	/// <param name="dateTimeProvider">The date time provider instance to use.</param>
	public AchievementsViewModel(ISteamApiService steamApiService, ISteamWebService steamWebService, IOptions<SteamSettings> options, IDateTimeProvider dateTimeProvider)
	{
		_steamApiService = steamApiService;
		_steamWebService = steamWebService;
		_steamSettings = options.Value;
		_dateTimeProvider = dateTimeProvider;
		_selectedGame = new();

		PropertyChanging += (s, e) => OnPropertyChanging(e.PropertyName);
		PropertyChanged += (s, e) => OnPropertyChanged(e.PropertyName);
	}

	/// <summary>
	/// The selected achievement to modify.
	/// </summary>
	[NotifyChanged(nameof(IsAchievementVisible), nameof(IsAchievementLockable), nameof(IsAchievementUnlockable))]
	public AchievementModel? SelectedAchievement
	{
		get => _selectedAchievement;
		set => SetProperty(ref _selectedAchievement, value);
	}

	/// <summary>
	/// The image of the selected achievement.
	/// </summary>
	public Image? AchievementImage
	{
		get => _achievementImage;
		private set => SetProperty(ref _achievementImage, value);
	}

	/// <summary>
	/// Indicates if the achievement can be visible.
	/// </summary>
	public bool IsAchievementVisible
		=> SelectedAchievement is not null;

	/// <summary>
	/// The game detail model instance to use.
	/// </summary>
	public GameDetailModel Model
	{
		get => _selectedGame;
		set => SetProperty(ref _selectedGame, value);
	}

	/// <summary>
	/// Indicates if the achievements are currently loading.
	/// </summary>
	public bool AchievementsAreLoading
	{
		get => _isAchievementsLoading;
		private set => SetProperty(ref _isAchievementsLoading, value);
	}

	/// <summary>
	/// Indicates if the game has achievements.
	/// </summary>
	public bool HasAchievements
	{
		get => _hasAchievements;
		set => SetProperty(ref _hasAchievements, value);
	}

	/// <summary>
	/// Indicates if the achievement can be locked.
	/// </summary>
	public bool IsAchievementLockable
		=> SelectedAchievement is not null && SelectedAchievement.Unlocked.IsTrue();

	/// <summary>
	/// Indicates if the achievement can be unlocked.
	/// </summary>
	public bool IsAchievementUnlockable
		=> SelectedAchievement is not null && SelectedAchievement.Unlocked.IsFalse();

	/// <summary>
	/// Indicates if the overall achievement progress in percent
	/// </summary>
	public float OverallAchievementProgress
	{
		get => _overallAchievementProgress;
		private set => SetProperty(ref _overallAchievementProgress, value);
	}

	/// <summary>
	/// The command to load the achievements for the selected game.
	/// </summary>
	public IAsyncActionCommand LoadAchievementsCommand
		=> new AsyncActionCommand(LoadAchievements, CanLoadAchievements);

	/// <summary>
	/// The command to lock the achievement.
	/// </summary>
	public IActionCommand<AchievementModel?> LockAchievementCommand
		=> new ActionCommand<AchievementModel?>(LockAchievement, CanLockAchievement);

	/// <summary>
	/// The command to unlock the achievement.
	/// </summary>
	public IActionCommand<AchievementModel?> UnlockAchievementCommand
		=> new ActionCommand<AchievementModel?>(UnlockAchievement, CanUnlockAchievement);

	private bool CanLoadAchievements()
		=> AchievementsAreLoading.IsFalse();

	private bool CanLockAchievement(AchievementModel? model)
		=> model is not null && model.Unlocked.IsTrue();

	private bool CanUnlockAchievement(AchievementModel? model)
		=> model is not null && model.Unlocked.IsFalse();

	private async Task LoadAchievements()
	{
		try
		{
			AchievementsAreLoading = true;

			Model.Achievements.Clear();

			IEnumerable<AchievementModel> achievementsData = await _steamWebService.GetAchievementsAsync(Model.Id, _steamSettings.ApiKey)
				.ConfigureAwait(true);

			foreach (AchievementModel achievementData in achievementsData)
			{
				(bool achieved, DateTime? unlockTime) = _steamApiService.GetAchievement(achievementData.Id);

				if (achieved)
				{
					achievementData.Unlocked = achieved;
					achievementData.UnlockedTime = unlockTime;
				}

				Model.Achievements.Add(achievementData);
			}

			HasAchievements = Model.Achievements.Count > 0;
		}
		finally
		{
			AchievementsAreLoading = false;
		}
	}

	private void LockAchievement(AchievementModel? model)
	{
		if (model is null)
			return;

		_ = _steamApiService.ResetAchievement(model.Id);
		_ = _steamApiService.StoreStats();

		SetAchievement(model, false, null);
	}

	private void UnlockAchievement(AchievementModel? model)
	{
		if (model is null)
			return;

		_ = _steamApiService.UnlockAchievement(model.Id);
		_ = _steamApiService.StoreStats();

		SetAchievement(model, true, _dateTimeProvider.Now);
	}

	private void OnPropertyChanging(string? propertyName)
	{
		if (propertyName is null)
			return;

		if (propertyName == nameof(Model))
		{
			if (_steamApiService.StatsRequested.IsTrue())
				_ = _steamApiService.StoreStats();

			if (_steamApiService.Initialized.IsTrue())
				_steamApiService.Shutdown();
		}
	}

	private void OnPropertyChanged(string? propertyName)
	{
		if (propertyName is null)
			return;

		if (propertyName == nameof(Model))
		{
			if (_steamApiService.Initialized.IsFalse())
				_ = _steamApiService.Initialize(Model.Id);

			if (_steamApiService.StatsRequested.IsFalse())
				_ = _steamApiService.RequestStats();

			HasAchievements = Model.Achievements.Count > 0;
			OverallAchievementProgress = CalculateOverallProgress(Model);
		}

		if (propertyName == nameof(SelectedAchievement) && SelectedAchievement is not null)
			AchievementImage = CreateImageFromUri(new(SelectedAchievement.ImageUrl));
	}

	private void SetAchievement(AchievementModel model, bool unlocked, DateTime? unlockedTime = null)
	{
		model.Unlocked = unlocked;
		model.UnlockedTime = unlockedTime;
		model.LastUpdate = _dateTimeProvider.Now;

		RaisePropertyChanged(nameof(IsAchievementLockable));
		RaisePropertyChanged(nameof(IsAchievementUnlockable));

		OverallAchievementProgress = CalculateOverallProgress(Model);

		AchievementImage = CreateImageFromUri(new(model.ImageUrl));
	}

	private static float CalculateOverallProgress(GameDetailModel model)
		=> model.Achievements.Count > 0 ? model.Achievements.Count(x => x.Unlocked) * 100f / model.Achievements.Count : 0f;
}
