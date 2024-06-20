namespace BB84.SAU.Domain.Interfaces.Models;

/// <summary>
/// The updatable model interface.
/// </summary>
public interface IUpdatable
{
	/// <summary>
	/// Indicates when the model was last updated.
	/// </summary>
	DateTime? LastUpdate { get; set; }
}
