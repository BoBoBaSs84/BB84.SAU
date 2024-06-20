using Microsoft.VisualStudio.TestTools.UnitTesting;

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
		string icon = "http://Test";
		string iconGray = "http://Test";

		model = new(id, name, description, hidden, icon, iconGray);

		Assert.IsNotNull(model);
		Assert.AreEqual(id, model.Id);
		Assert.AreEqual(name, model.Name);
		Assert.AreEqual(description, model.Description);
		Assert.AreEqual(hidden, model.Hidden);
		Assert.AreEqual(icon, model.Icon);
		Assert.AreEqual(iconGray, model.IconGray);
		Assert.AreEqual(false, model.Unlocked);
		Assert.IsNull(model.UnlockedTime);
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
		string icon = "http://Test";
		string iconGray = "http://Test";
		bool unlocked = true;
		DateTime unlockedDate = DateTime.MinValue;

		model.Id = id;
		model.Name = name;
		model.Description = description;
		model.Hidden = hidden;
		model.Icon = icon;
		model.IconGray = iconGray;
		model.Unlocked = unlocked;
		model.UnlockedTime = unlockedDate;

		Assert.AreEqual(id, model.Id);
		Assert.AreEqual(name, model.Name);
		Assert.AreEqual(description, model.Description);
		Assert.AreEqual(hidden, model.Hidden);
		Assert.AreEqual(icon, model.Icon);
		Assert.AreEqual(iconGray, model.IconGray);
		Assert.AreEqual(unlocked, model.Unlocked);
		Assert.AreEqual(unlockedDate, model.UnlockedTime);
	}
}
