using BB84.SAU.Application.Services;
using BB84.SAU.Application.ViewModels;

namespace BB84.SAU.Application.Tests.Services;

[TestClass]
public sealed class NavigationServiceTests
{
	[TestMethod]
	public void NavigateToTest()
	{
		AboutViewModel viewModel = new(new());
		NavigationService service = new(t => viewModel);

		service.NavigateTo<AboutViewModel>();

		Assert.IsInstanceOfType(service.CurrentView, typeof(AboutViewModel));
	}
}
