// -----------------------------------------------------------------------------
// Copyright:	Robert Peter Meyer
// License:		MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------------
using System.Collections.ObjectModel;

using BB84.SAU.Domain.Interfaces.Models;
using BB84.SAU.Domain.Models.Base;

namespace BB84.SAU.Domain.Models;

/// <summary>
/// The game detail model class.
/// </summary>
/// <param name="id">The identifier of the game.</param>
/// <param name="title">The title of th game.</param>
/// <param name="description">The short description of the game.</param>
/// <param name="imageUrl">The image url of the game.</param>
/// <param name="lastUpdate">Indicates when the game data was last updated.</param>

public sealed class GameDetailModel(int id, string title, string? description = null, string? imageUrl = null, DateTime? lastUpdate = null) : ModelBase, IUpdatable
{
	private ObservableCollection<AchievementModel> _achievements = [];

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
		get => id;
		set => SetProperty(ref id, value);
	}

	/// <summary>
	/// The title of th game.
	/// </summary>
	public string Title
	{
		get => title;
		set => SetProperty(ref title, value);
	}

	/// <summary>
	/// The short description of the game.
	/// </summary>
	public string? Description
	{
		get => description;
		set => SetProperty(ref description, value);
	}

	/// <summary>
	/// The image url of the game.
	/// </summary>
	public string? ImageUrl
	{
		get => imageUrl;
		set => SetProperty(ref imageUrl, value);
	}

	/// <summary>
	/// Indicates when the game data was last updated.
	/// </summary>
	public DateTime? LastUpdate
	{
		get => lastUpdate;
		set => SetProperty(ref lastUpdate, value);
	}

	/// <summary>
	/// The achievements of the game.
	/// </summary>
	public ObservableCollection<AchievementModel> Achievements
	{
		get => _achievements;
		set => SetProperty(ref _achievements, value);
	}
}
