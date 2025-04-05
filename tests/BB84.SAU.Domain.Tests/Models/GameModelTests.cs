// -----------------------------------------------------------------------------
// Copyright:	Robert Peter Meyer
// License:		MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------------
using BB84.SAU.Domain.Models;

namespace BB84.SAU.Domain.Tests.Models;

[TestClass()]
public class GameModelTests
{
	[TestMethod]
	[TestCategory("Constructor")]
	public void GameModelTest()
	{
		GameModel? model;
		int id = 1;
		string title = "UnitTest";

		model = new(id, title);

		Assert.IsNotNull(model);
		Assert.AreEqual(id, model.Id);
		Assert.AreEqual(title, model.Title);
	}

	[TestMethod]
	public void GameModelSetValuesTest()
	{
		GameModel model = new(0, string.Empty);
		int id = 1;
		string title = "UnitTest";

		model.Id = id;
		model.Title = title;

		Assert.AreEqual(id, model.Id);
		Assert.AreEqual(title, model.Title);
	}
}
