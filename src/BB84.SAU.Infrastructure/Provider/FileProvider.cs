using System.Diagnostics.CodeAnalysis;

using BB84.SAU.Infrastructure.Interfaces.Provider;

namespace BB84.SAU.Infrastructure.Provider;

/// <summary>
/// The file provider class.
/// </summary>
[ExcludeFromCodeCoverage(Justification = "Wrapper/Astraction class")]
internal sealed class FileProvider : IFileProvider
{
	public void Delete(string path)
		=> File.Delete(path);

	public void WriteAllText(string path, string? contents)
		=> File.WriteAllText(path, contents);
}
