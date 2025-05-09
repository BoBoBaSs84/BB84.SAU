﻿// -----------------------------------------------------------------------------
// Copyright:	Robert Peter Meyer
// License:		MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------------
namespace BB84.SAU.Domain.Interfaces.Models;

/// <summary>
/// The updatable model interface.
/// </summary>
public interface IUpdatable
{
	/// <summary>
	/// Indicates when the model was last updated.
	/// </summary>
	DateTime? LastUpdate { get; set; }
}
