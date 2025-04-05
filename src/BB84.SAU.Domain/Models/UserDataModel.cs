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
/// The user profile model class.
/// </summary>
/// <param name="name">The name of the user.</param>
/// <param name="imageUrl">The image url of the user.</param>
/// <param name="profileUrl">The uniform resource locator of the user profile.</param>
/// <param name="created">The profile creation date time.</param>
/// <param name="lastLogOff">The last log off of the profile.</param>
/// <param name="lastUpdate">Indicates when the user data was last updated.</param>
public sealed class UserDataModel(string name, string imageUrl, string? profileUrl, DateTime created, DateTime lastLogOff, DateTime? lastUpdate = null) : ModelBase, IUpdatable
{
	private ObservableCollection<GameDetailModel> _games = [];

	/// <summary>
	/// Initializes a new instance of the <see cref="UserDataModel"/> class.
	/// </summary>
	public UserDataModel() : this(string.Empty, string.Empty, string.Empty, default, default)
	{ }

	/// <summary>
	/// The name of the user.
	/// </summary>
	public string Name
	{
		get => name;
		set => SetProperty(ref name, value);
	}

	/// <summary>
	/// The image url of the user.
	/// </summary>
	public string ImageUrl
	{
		get => imageUrl;
		set => SetProperty(ref imageUrl, value);
	}

	/// <summary>
	/// The uniform resource locator of the user profile.
	/// </summary>
	public string? ProfileUrl
	{
		get => profileUrl;
		set => SetProperty(ref profileUrl, value);
	}

	/// <summary>
	/// The profile creation date time.
	/// </summary>
	public DateTime Created
	{
		get => created;
		set => SetProperty(ref created, value);
	}

	/// <summary>
	/// The last log off of the profile.
	/// </summary>
	public DateTime LastLogOff
	{
		get => lastLogOff;
		set => SetProperty(ref lastLogOff, value);
	}

	/// <summary>
	/// Indicates when the user data was last updated.
	/// </summary>
	public DateTime? LastUpdate
	{
		get => lastUpdate;
		set => SetProperty(ref lastUpdate, value);
	}

	/// <summary>
	/// The games of the user.
	/// </summary>
	public ObservableCollection<GameDetailModel> Games
	{
		get => _games;
		set => SetProperty(ref _games, value);
	}
}
