namespace BB84.SAU.Domain.Models;

/// <summary>
/// The logbook model class.
/// </summary>
/// <param name="message">The message of the logbook entry.</param>
public sealed class LogbookModel(string message)
{
	/// <summary>
	/// The identifier of the logbook entry.
	/// </summary>
	public Guid Id { get; } = Guid.NewGuid();

	/// <summary>
	/// The coordinated universal time of the logbook entry.
	/// </summary>
	public DateTime DateTimeUtc { get; } = DateTime.UtcNow;

	/// <summary>
	/// The message of the logbook entry.
	/// </summary>
	public string Message { get; } = message;
}
