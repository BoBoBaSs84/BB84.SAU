// -----------------------------------------------------------------------------
// Copyright:	Robert Peter Meyer
// License:		MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------------
namespace BB84.SAU.Domain.Exceptions;

/// <summary>
/// The steam sdk exception class.
/// </summary>
/// <inheritdoc/>
public sealed class SteamSdkException(string? message, Exception? innerException = null) : Exception(message, innerException)
{
	/// <summary>
	/// Initializes a new instance of the <see cref="SteamSdkException"/> class with a specified error message.
	/// </summary>
	/// <inheritdoc/>
	public SteamSdkException(string? message) : this(message, null)
	{ }
}
