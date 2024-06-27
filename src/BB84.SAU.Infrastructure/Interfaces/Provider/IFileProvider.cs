namespace BB84.SAU.Infrastructure.Interfaces.Provider;

/// <summary>
/// The file provider interface.
/// Serves for the abstraction of <see cref="File"/> methods.
/// </summary>
internal interface IFileProvider
{
	/// <inheritdoc cref="File.WriteAllText(string, string?)"/>
	void WriteAllText(string path, string? contents);

	/// <inheritdoc cref="File.Delete(string)"/>
	void Delete(string path);
}
