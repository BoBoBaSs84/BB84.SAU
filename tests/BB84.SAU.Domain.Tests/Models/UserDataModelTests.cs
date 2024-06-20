using Microsoft.VisualStudio.TestTools.UnitTesting;

using BB84.SAU.Domain.Models;

namespace BB84.SAU.Domain.Tests.Models;

[TestClass]
public sealed class UserDataModelTests
{
	[TestMethod]
	[TestCategory("Constructor")]
	public void UserDataModelTest()
	{
		UserDataModel? model;
		string name = "TestName",
			imageUrl = "ImageUrl",
			url = "Url";
		DateTime created = DateTime.MaxValue,
			lastLogOff = DateTime.MaxValue,
			lastUpdate = DateTime.MaxValue;

		model = new(name, imageUrl, url, created, lastLogOff, lastUpdate);

		Assert.IsNotNull(model);
		Assert.AreEqual(string.Empty, model.Name);
		Assert.AreEqual(string.Empty, model.ImageUrl);
		Assert.IsNull(model.ProfileUrl);
		Assert.AreEqual(DateTime.MinValue, model.Created);
		Assert.AreEqual(DateTime.MinValue, model.LastLogOff);
		Assert.IsNull(model.LastUpdate);
	}

	[TestMethod]
	public void UserDataModelSetValuesTest()
	{
		UserDataModel model = new();
		string name = "TestName",
			imageUrl = "ImageUrl",
			url = "Url";
		DateTime created = DateTime.MaxValue,
			lastLogOff = DateTime.MaxValue,
			lastUpdate = DateTime.MaxValue;

		model.Name = name;
		model.ImageUrl = imageUrl;
		model.ProfileUrl = url;
		model.Created = created;
		model.LastLogOff = lastLogOff;
		model.LastUpdate = lastUpdate;

		Assert.IsNotNull(model);
		Assert.AreEqual(name, model.Name);
		Assert.AreEqual(imageUrl, model.ImageUrl);
		Assert.AreEqual(url, model.ProfileUrl);
		Assert.AreEqual(created, model.Created);
		Assert.AreEqual(lastLogOff, model.LastLogOff);
		Assert.AreEqual(lastUpdate, model.LastUpdate);
	}
}
