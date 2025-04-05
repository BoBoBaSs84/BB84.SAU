// -----------------------------------------------------------------------------
// Copyright:	Robert Peter Meyer
// License:		MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------------
using System.Diagnostics.CodeAnalysis;

using BB84.SAU.Application.Interfaces.Application.Provider;

namespace BB84.SAU.Application.Provider;

/// <summary>
/// The date time provider class.
/// </summary>
[ExcludeFromCodeCoverage(Justification = "Wrapper for DateTime")]
internal sealed class DateTimeProvider : IDateTimeProvider
{
	public DateTime Now => DateTime.Now;

	public DateTime Today => DateTime.Today;
}
