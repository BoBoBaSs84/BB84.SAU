// -----------------------------------------------------------------------------
// Copyright:	Robert Peter Meyer
// License:		MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------------
namespace BB84.SAU.Application.Interfaces.Application.Provider;

/// <summary>
/// The date time provider interface.
/// </summary>
public interface IDateTimeProvider
{
	/// <summary>
	/// Gets a System.DateTime object that is set to the current date and time on
	/// this computer, expressed as the local time.
	/// </summary>
	DateTime Now { get; }

	/// <summary>
	/// Returns a DateTime representing the current date. The date part of the returned
	/// value is the current date, and the time-of-day part of the returned value is zero (midnight).
	/// </summary>
	DateTime Today { get; }
}
