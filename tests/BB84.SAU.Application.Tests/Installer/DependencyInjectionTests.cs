using System.Diagnostics.CodeAnalysis;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using BB84.SAU.Application.Installer;

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

		Assert.AreEqual(41, services.Count);
	}
}
