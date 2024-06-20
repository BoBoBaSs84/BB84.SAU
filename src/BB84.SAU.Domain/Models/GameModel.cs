using BB84.SAU.Domain.Models.Base;

namespace BB84.SAU.Domain.Models;

/// <summary>
/// The game model class.
/// </summary>
public sealed class GameModel(int id, string title) : ModelBase
{
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
}
