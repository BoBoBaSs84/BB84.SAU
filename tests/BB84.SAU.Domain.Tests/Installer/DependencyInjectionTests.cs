using System.Diagnostics.CodeAnalysis;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using BB84.SAU.Domain.Installer;

namespace BB84.SAU.Domain.Tests.Installer;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, unit test.")]
public sealed class DependencyInjectionTests
{
	[TestMethod]
	[TestCategory("DependencyInjection")]
	public void RegisterDomainServicesTest()
	{
		IServiceCollection services = new ServiceCollection();

		services.RegisterDomainServices();

		Assert.AreEqual(2, services.Count);
	}
}
