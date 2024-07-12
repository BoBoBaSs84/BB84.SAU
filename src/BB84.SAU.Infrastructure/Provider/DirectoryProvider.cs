using System.Diagnostics.CodeAnalysis;

using BB84.SAU.Infrastructure.Interfaces.Provider;

namespace BB84.SAU.Infrastructure.Provider;

/// <summary>
/// The directory provider class.
/// </summary>
[ExcludeFromCodeCoverage(Justification = "Wrapper/Astraction class")]
internal sealed class DirectoryProvider : IDirectoryProvider
{
	public DirectoryInfo CreateDirectory(string path)
		=> Directory.CreateDirectory(path);
}
