// -----------------------------------------------------------------------------
// Copyright:	Robert Peter Meyer
// License:		MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------------
namespace BB84.SAU.Infrastructure.Interfaces.Provider;

/// <summary>
/// The file provider interface.
/// Serves for the abstraction of <see cref="File"/> methods.
/// </summary>
internal interface IFileProvider
{
	/// <inheritdoc cref="File.Delete(string)"/>
	void Delete(string path);

	/// <inheritdoc cref="File.Exists(string?)"/>
	bool Exists(string? path);

	/// <inheritdoc cref="File.ReadAllTextAsync(string, CancellationToken)"/>
	Task<string> ReadAllTextAsync(string path, CancellationToken cancellationToken = default);

	/// <inheritdoc cref="File.WriteAllText(string, string?)"/>
	void WriteAllText(string path, string? contents);

	/// <inheritdoc cref="File.WriteAllTextAsync(string, string?, CancellationToken)"/>
	Task WriteAllTextAsync(string path, string? contents, CancellationToken cancellationToken = default);
}
