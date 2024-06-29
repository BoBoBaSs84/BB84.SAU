using BB84.SAU.Domain.Models;

namespace BB84.SAU.Domain.Tests.Models;

[TestClass]
public sealed class AchievementModelTests
{
	[TestMethod]
	[TestCategory("Constructor")]
	public void AchievementModelTest()
	{
		AchievementModel? model;
		string id = "Id";
		string name = "UnitTest";
		string? description = "UnitTestDescription";
		bool hidden = false;
		string icon = "http://Test/icon";
		string iconGray = "http://Test/iconGray";

		model = new(id, name, description, hidden, icon, iconGray);

		Assert.IsNotNull(model);
		Assert.AreEqual(id, model.Id);
		Assert.AreEqual(name, model.Name);
		Assert.AreEqual(description, model.Description);
		Assert.AreEqual(hidden, model.Hidden);
		Assert.AreEqual(icon, model.Icon);
		Assert.AreEqual(iconGray, model.IconGray);
		Assert.AreEqual(iconGray, model.ImageUrl);
		Assert.AreEqual(false, model.Unlocked);
		Assert.IsNull(model.UnlockedTime);
		Assert.IsNull(model.LastUpdate);
	}

	[TestMethod]
	[TestCategory("Constructor")]
	public void AchievementModelSetValuesTest()
	{
		AchievementModel model = new(string.Empty, string.Empty, string.Empty, default, string.Empty, string.Empty);
		string id = "Id";
		string name = "UnitTest";
		string? description = "UnitTestDescription";
		bool hidden = false;
		string icon = "http://Test/icon";
		string iconGray = "http://Test/iconGray";
		bool unlocked = true;
		DateTime unlockedDate = DateTime.MinValue;
		DateTime lastUpdate = DateTime.MinValue;

		model.Id = id;
		model.Name = name;
		model.Description = description;
		model.Hidden = hidden;
		model.Icon = icon;
		model.IconGray = iconGray;
		model.Unlocked = unlocked;
		model.UnlockedTime = unlockedDate;
		model.LastUpdate = lastUpdate;

		Assert.AreEqual(id, model.Id);
		Assert.AreEqual(name, model.Name);
		Assert.AreEqual(description, model.Description);
		Assert.AreEqual(hidden, model.Hidden);
		Assert.AreEqual(icon, model.Icon);
		Assert.AreEqual(iconGray, model.IconGray);
		Assert.AreEqual(icon, model.ImageUrl);
		Assert.AreEqual(unlocked, model.Unlocked);
		Assert.AreEqual(unlockedDate, model.UnlockedTime);
		Assert.AreEqual(lastUpdate, model.LastUpdate);
	}
}
