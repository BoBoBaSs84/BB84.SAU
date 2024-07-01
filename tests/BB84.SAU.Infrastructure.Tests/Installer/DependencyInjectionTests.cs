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
	public void RegisterInfrastructureServicesTest()
	{
		Mock<IHostEnvironment> env = new Mock<IHostEnvironment>()
			.SetupAllProperties();
		env.Setup(x => x.EnvironmentName).Returns("Development");
		IServiceCollection services = new ServiceCollection();

		services.RegisterInfrastructureServices(env.Object);

		Assert.AreEqual(16, services.Count);
	}
}
