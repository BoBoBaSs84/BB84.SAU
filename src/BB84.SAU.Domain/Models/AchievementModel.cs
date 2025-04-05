// -----------------------------------------------------------------------------
// Copyright:	Robert Peter Meyer
// License:		MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------------
using System.Text.Json.Serialization;

using BB84.SAU.Domain.Interfaces.Models;
using BB84.SAU.Domain.Models.Base;

namespace BB84.SAU.Domain.Models;

/// <summary>
/// The achievement model class.
/// </summary>
/// <param name="id">The identifier of the achievement.</param>
/// <param name="name">The name of the achievement.</param>
/// <param name="description">The description of the achievement.</param>
/// <param name="hidden">Is the achievement hidden?</param>
/// <param name="icon">The unlocked icon url of the achievement.</param>
/// <param name="iconGray">The locked icon url of the achievement.</param>
public sealed class AchievementModel(string id, string name, string? description, bool hidden, string icon, string iconGray) : ModelBase, IUpdatable
{
	private bool _unlocked;
	private DateTime? _unlockedDate;
	private DateTime? _lastUpdate;

	/// <summary>
	/// The identifier of the achievement.
	/// </summary>
	public string Id
	{
		get => id;
		set => SetProperty(ref id, value);
	}

	/// <summary>
	/// The display name of the achievement.
	/// </summary>
	public string Name
	{
		get => name;
		set => SetProperty(ref name, value);
	}

	/// <summary>
	/// The description of the achievement.
	/// </summary>
	public string? Description
	{
		get => description;
		set => SetProperty(ref description, value);
	}

	/// <summary>
	/// Is the achievement hidden?
	/// </summary>
	public bool Hidden
	{
		get => hidden;
		set => SetProperty(ref hidden, value);
	}

	/// <summary>
	/// The unlocked icon url of the achievement.
	/// </summary>
	public string Icon
	{
		get => icon;
		set => SetProperty(ref icon, value);
	}

	/// <summary>
	/// The locked icon url of the achievement.
	/// </summary>
	public string IconGray
	{
		get => iconGray;
		set => SetProperty(ref iconGray, value);
	}

	/// <summary>
	/// The effective icon url of the achievement.
	/// </summary>
	[JsonIgnore]
	public string ImageUrl => Unlocked ? Icon : IconGray;

	/// <summary>
	/// Has the achievement been unlocked?
	/// </summary>
	public bool Unlocked
	{
		get => _unlocked;
		set => SetProperty(ref _unlocked, value);
	}

	/// <summary>
	/// The unlocked date of the achievement.
	/// </summary>
	public DateTime? UnlockedTime
	{
		get => _unlockedDate;
		set => SetProperty(ref _unlockedDate, value);
	}

	/// <summary>
	/// Indicates when the achievement data was last updated.
	/// </summary>
	public DateTime? LastUpdate
	{
		get => _lastUpdate;
		set => SetProperty(ref _lastUpdate, value);
	}
}
