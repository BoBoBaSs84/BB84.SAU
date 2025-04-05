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
public sealed class LogbookModelTests
{
	[TestMethod]
	[TestCategory("Constructor")]
	public void LogbookModelTest()
	{
		LogbookModel? model;
		string message = "Unit Test Message";

		model = new LogbookModel(message);

		Assert.IsNotNull(model);
		Assert.AreEqual(message, model.Message);
		Assert.AreNotEqual(Guid.Empty, model.Id);
		Assert.AreNotEqual(DateTime.MinValue, model.DateTimeUtc);
	}
}
