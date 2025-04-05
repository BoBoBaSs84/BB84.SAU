// -----------------------------------------------------------------------------
// Copyright:	Robert Peter Meyer
// License:		MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------------
using BB84.SAU.Application.Interfaces.Application.Services;
using BB84.SAU.Application.ViewModels;

using Moq;

namespace BB84.SAU.Application.Tests.ViewModels;

[TestClass]
public sealed class MainViewModelTests
{
	[TestMethod]
	[TestCategory("Constructor")]
	public void MainViewModelTest()
	{
		Mock<INavigationService> navigationServiceMock = new();

		MainViewModel viewModel = new(navigationServiceMock.Object);

		Assert.IsNotNull(viewModel);
		Assert.AreEqual(navigationServiceMock.Object, viewModel.NavigationService);
	}
}
