using BB84.SAU.Domain.Models;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BB84.SAU.Domain.Tests.Models;

[TestClass]
public sealed class GameDetailModelTests
{
	[TestMethod]
	[TestCategory("Constructor")]
	public void GameDetailModelTest()
	{
		GameDetailModel? model;
		int id = 1;
		string title = "UnitTest";
		string description = "UnitTestDescription";
		string imageUrl = "http://unittest.com";

		model = new(id, title, description, imageUrl);

		Assert.IsNotNull(model);
		Assert.AreEqual(id, model.Id);
		Assert.AreEqual(title, model.Title);
		Assert.AreEqual(description, model.Description);
		Assert.AreEqual(imageUrl, model.ImageUrl);
		Assert.IsNull(model.LastUpdate);
		Assert.AreEqual(0, model.Achievements.Count);
	}

	[TestMethod]
	public void GameDetailModelSetValuesTest()
	{
		GameDetailModel? model = new();
		int id = 1;
		string title = "UnitTest";
		string description = "UnitTestDescription";
		string imageUrl = "http://unittest.com";
		DateTime lastUpdate = DateTime.MinValue;

		model.Id = id;
		model.Title = title;
		model.Description = description;
		model.ImageUrl = imageUrl;
		model.LastUpdate = lastUpdate;

		Assert.AreEqual(id, model.Id);
		Assert.AreEqual(title, model.Title);
		Assert.AreEqual(description, model.Description);
		Assert.AreEqual(imageUrl, model.ImageUrl);
		Assert.AreEqual(lastUpdate, model.LastUpdate);
	}
}
