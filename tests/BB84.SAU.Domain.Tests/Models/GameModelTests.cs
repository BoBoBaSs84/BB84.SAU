using BB84.SAU.Domain.Models;

using Microsoft.VisualStudio.TestTools.UnitTesting;

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
