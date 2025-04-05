// -----------------------------------------------------------------------------
// Copyright:	Robert Peter Meyer
// License:		MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------------
using BB84.SAU.Application.ViewModels;
using BB84.SAU.Domain.Models;

namespace BB84.SAU.Application.Tests.ViewModels;

[TestClass]
public sealed class AboutViewModelTests
{
	[TestMethod]
	[TestCategory("Constructor")]
	public void AboutViewModelTest()
	{
		AboutModel model = new();

		AboutViewModel viewModel = new(model);

		Assert.IsNotNull(viewModel);
		Assert.AreEqual(model, viewModel.Model);
	}
}
