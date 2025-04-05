// -----------------------------------------------------------------------------
// Copyright:	Robert Peter Meyer
// License:		MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------------
using BB84.Extensions;
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
