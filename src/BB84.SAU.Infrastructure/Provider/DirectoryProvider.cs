// -----------------------------------------------------------------------------
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
/// The directory provider class.
/// </summary>
[ExcludeFromCodeCoverage(Justification = "Wrapper/Astraction class")]
internal sealed class DirectoryProvider : IDirectoryProvider
{
	public DirectoryInfo CreateDirectory(string path)
		=> Directory.CreateDirectory(path);
}
