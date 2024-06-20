using System.ComponentModel.DataAnnotations;

using BB84.Notifications;

namespace BB84.SAU.Domain.Settings;

/// <summary>
/// The strongly typed steam settings.
/// </summary>
public sealed class SteamSettings : ValidatableObject
{
	private long _id;
	private string _apiKey = string.Empty;

	/// <summary>
	/// The steam user identifier.
	/// </summary>
	[Range(long.MinValue, long.MaxValue)]
	public long Id
	{
		get => _id;
		set => SetPropertyAndValidate(ref _id, value);
	}

	/// <summary>
	/// The steam user api key.
	/// </summary>
	[StringLength(32, MinimumLength = 32)]
	public string ApiKey
	{
		get => _apiKey;
		set => SetPropertyAndValidate(ref _apiKey, value);
	}
}
