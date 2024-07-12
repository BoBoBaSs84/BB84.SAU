namespace BB84.SAU.Infrastructure.Interfaces.Provider;

/// <summary>
/// The file provider interface.
/// Serves for the abstraction of <see cref="Directory"/> methods.
/// </summary>
internal interface IDirectoryProvider
{
	/// <inheritdoc cref="Directory.CreateDirectory(string)"/>
	DirectoryInfo CreateDirectory(string path);
}
