// -----------------------------------------------------------------------------
// Copyright:	Robert Peter Meyer
// License:		MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------------
using BB84.SAU.Infrastructure.Installer;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Moq;

namespace BB84.SAU.Infrastructure.Tests.Installer;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, unit test.")]
public class DependencyInjectionTests
{
	[TestMethod]
	[TestCategory("DependencyInjection")]
	public void RegisterInfrastructureServicesDevelopmentTest()
	{
		Mock<IHostEnvironment> env = new Mock<IHostEnvironment>()
			.SetupAllProperties();
		env.Setup(x => x.EnvironmentName).Returns("Development");
		IServiceCollection services = new ServiceCollection();

		services.RegisterInfrastructureServices(env.Object);

		Assert.AreEqual(29, services.Count);
	}

	[TestMethod]
	[TestCategory("DependencyInjection")]
	public void RegisterInfrastructureServicesProductionTest()
	{
		Mock<IHostEnvironment> env = new Mock<IHostEnvironment>()
			.SetupAllProperties();
		env.Setup(x => x.EnvironmentName).Returns("Production");
		IServiceCollection services = new ServiceCollection();

		services.RegisterInfrastructureServices(env.Object);

		Assert.AreEqual(17, services.Count);
	}
}
