// -----------------------------------------------------------------------------
// Copyright:	Robert Peter Meyer
// License:		MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------------
using BB84.SAU.Application.Installer;

using Microsoft.Extensions.DependencyInjection;

namespace BB84.SAU.Application.Tests.Installer;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, unit test.")]
public sealed class DependencyInjectionTests
{
	[TestMethod]
	[TestCategory("DependencyInjection")]
	public void RegisterApplicationServicesTest()
	{
		IServiceCollection services = new ServiceCollection();

		services.RegisterApplicationServices();

		Assert.AreEqual(40, services.Count);
	}
}
