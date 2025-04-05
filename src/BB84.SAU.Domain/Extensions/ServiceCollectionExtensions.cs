// -----------------------------------------------------------------------------
// Copyright:	Robert Peter Meyer
// License:		MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------------
using System.Diagnostics.CodeAnalysis;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using BB84.SAU.Domain.Models;

namespace BB84.SAU.Domain.Extensions;

/// <summary>
/// The service collection extensions class.
/// </summary>
[ExcludeFromCodeCoverage(Justification = "Dependency injection helper.")]
internal static class ServiceCollectionExtensions
{
	/// <summary>
	/// Registers the required models to the service collection.
	/// </summary>
	/// <param name="services">The service collection to enrich.</param>
	/// <returns>The enriched service collection.</returns>
	internal static IServiceCollection RegisterModels(this IServiceCollection services)
	{
		services.TryAddSingleton<AboutModel>();
		services.TryAddSingleton<UserDataModel>();

		return services;
	}
}
