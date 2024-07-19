using System.Collections.ObjectModel;

using BB84.SAU.Domain.Interfaces.Models;
using BB84.SAU.Domain.Models.Base;

namespace BB84.SAU.Domain.Models;

/// <summary>
/// The game detail model class.
/// </summary>

public sealed class GameDetailModel : ModelBase, IUpdatable
{
	private int _id;
	private string _title;
	private string? _description;
	private string? _imageUrl;
	private DateTime? _lastUpdate;
	private ObservableCollection<AchievementModel> _achievements;

	/// <summary>
	/// Initializes a new instance of the <see cref="GameDetailModel"/> class.
	/// </summary>
	/// <param name="id">The identifier of the game.</param>
	/// <param name="title">The title of th game.</param>
	/// <param name="description">The short description of the game.</param>
	/// <param name="imageUrl">The image url of the game.</param>
	/// <param name="lastUpdate">Indicates when the game data was last updated.</param>
	public GameDetailModel(int id, string title, string? description = null, string? imageUrl = null, DateTime? lastUpdate = null)
	{
		_id = id;
		_title = title;
		_description = description;
		_imageUrl = imageUrl;
		_lastUpdate = lastUpdate;
		_achievements = [];

		PropertyChanged += (s, e) => OnAchievementsChanged(e.PropertyName);
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="GameDetailModel"/> class.
	/// </summary>
	public GameDetailModel() : this(default, string.Empty)
	{ }

	/// <summary>
	/// The identifier of the game.
	/// </summary>
	public int Id
	{
		get => _id;
		set => SetProperty(ref _id, value);
	}

	/// <summary>
	/// The title of th game.
	/// </summary>
	public string Title
	{
		get => _title;
		set => SetProperty(ref _title, value);
	}

	/// <summary>
	/// The short description of the game.
	/// </summary>
	public string? Description
	{
		get => _description;
		set => SetProperty(ref _description, value);
	}

	/// <summary>
	/// The image url of the game.
	/// </summary>
	public string? ImageUrl
	{
		get => _imageUrl;
		set => SetProperty(ref _imageUrl, value);
	}

	/// <summary>
	/// Indicates when the game data was last updated.
	/// </summary>
	public DateTime? LastUpdate
	{
		get => _lastUpdate;
		set => SetProperty(ref _lastUpdate, value);
	}

	/// <summary>
	/// The amount of achievements that are unlocked.
	/// </summary>
	public int AchievementsUnlocked
		=> Achievements.Count(x => x.Unlocked.Equals(true));

	/// <summary>
	/// The amount of achievements.
	/// </summary>
	public int AchievementsCount
		=> Achievements.Count;

	/// <summary>
	/// The overall achievements progress.
	/// </summary>
	public float AchievementsProgress
		=> HasAchievements ? AchievementsUnlocked * 100f / AchievementsCount : 0f;

	/// <summary>
	/// Indicates if the game has any achievements.
	/// </summary>
	public bool HasAchievements
		=> Achievements.Count > 0;

	/// <summary>
	/// The achievements of the game.
	/// </summary>
	public ObservableCollection<AchievementModel> Achievements
	{
		get => _achievements;
		set
		{
			foreach (var achievement in _achievements)
				achievement.PropertyChanged -= (s, e) => OnAchievementUnlocked(e.PropertyName);

			SetProperty(ref _achievements, value);

			foreach (var achievement in _achievements)
				achievement.PropertyChanged += (s, e) => OnAchievementUnlocked(e.PropertyName);
		}
	}

	private void OnAchievementsChanged(string? propertyName)
	{
		if (propertyName is not null && propertyName == nameof(Achievements))
		{
			RaisePropertyChanged(nameof(AchievementsCount));
			RaisePropertyChanged(nameof(HasAchievements));
		}
	}

	private void OnAchievementUnlocked(string? propertyName)
	{
		if (propertyName is not null && propertyName == nameof(AchievementModel.Unlocked))
		{
			RaisePropertyChanged(nameof(AchievementsUnlocked));
			RaisePropertyChanged(nameof(AchievementsProgress));
		}
	}
}
