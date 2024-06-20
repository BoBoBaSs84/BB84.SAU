using BB84.Extensions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using BB84.SAU.Domain.Settings;

namespace BB84.SAU.Domain.Tests.Settings;

[TestClass]
public sealed class SteamSettingsTests
{
	[TestMethod]
	public void SetInvalidParametersTest()
	{
		long id = 1023978410298751L;
		string apiKey = "test";

		SteamSettings settings = new()
		{
			Id = id,
			ApiKey = apiKey
		};

		Assert.IsTrue(settings.HasErrors);
		Assert.AreEqual(id, settings.Id);
		Assert.AreEqual(apiKey, settings.ApiKey);
	}

	[TestMethod]
	public void SetValidParametersTest()
	{
		SteamSettings settings = new()
		{
			Id = 1023978410298751L,
			ApiKey = string.Concat("SuperFanceUnitTestString").GetMd5Utf8()
		};

		Assert.IsFalse(settings.HasErrors);
	}
}
