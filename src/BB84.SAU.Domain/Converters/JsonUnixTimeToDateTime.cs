using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BB84.SAU.Domain.Converters;

/// <summary>
/// The json unix time to date time converter class.
/// </summary>
public sealed class JsonUnixTimeToDateTime : JsonConverter<DateTime>
{
	/// <inheritdoc/>
	public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		long unixTimeSeconds = reader.GetInt64();
		return DateTimeOffset.FromUnixTimeSeconds(unixTimeSeconds).LocalDateTime;
	}

	/// <inheritdoc/>
	public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
	{
		long unixTimeSeconds = new DateTimeOffset(value).ToUnixTimeSeconds();
		writer.WriteStringValue(unixTimeSeconds.ToString(CultureInfo.InvariantCulture));
	}
}
