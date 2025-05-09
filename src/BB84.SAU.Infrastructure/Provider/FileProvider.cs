﻿// -----------------------------------------------------------------------------
// Copyright:	Robert Peter Meyer
// License:		MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------------
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

	public bool Exists(string? path)
		=> File.Exists(path);

	public Task<string> ReadAllTextAsync(string path, CancellationToken cancellationToken = default)
		=> File.ReadAllTextAsync(path, cancellationToken);

	public void WriteAllText(string path, string? contents)
		=> File.WriteAllText(path, contents);

	public Task WriteAllTextAsync(string path, string? contents, CancellationToken cancellationToken = default)
		=> File.WriteAllTextAsync(path, contents, cancellationToken);
}
