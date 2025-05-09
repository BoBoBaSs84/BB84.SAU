﻿// -----------------------------------------------------------------------------
// Copyright:	Robert Peter Meyer
// License:		MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------------
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
