// -----------------------------------------------------------------------------
// Copyright:	Robert Peter Meyer
// License:		MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------------
using BB84.SAU.Domain.Models;

namespace BB84.SAU.Domain.Tests.Models;

[TestClass]
[TestCategory("Constructor")]
public sealed class AboutModelTests
{
	[TestMethod]
	public void AboutModelTest()
	{
		AboutModel? model;

		model = new();

		Assert.IsNotNull(model);
		Assert.IsNotNull(model.Title);
		Assert.IsNotNull(model.Version);
		Assert.IsNotNull(model.Comments);
		Assert.IsNotNull(model.Company);
		Assert.IsNotNull(model.Copyright);
		Assert.IsNotNull(model.FrameworkName);
		Assert.IsNotNull(model.Repository);
	}
}
