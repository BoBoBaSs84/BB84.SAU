// -----------------------------------------------------------------------------
// Copyright:	Robert Peter Meyer
// License:		MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------------
using BB84.SAU.Application.Interfaces.Application.Services;
using BB84.SAU.Application.ViewModels;
using BB84.SAU.Domain.Settings;

using Microsoft.Extensions.Options;

using Moq;

namespace BB84.SAU.Application.Tests.ViewModels;

[TestClass]
public sealed class SettingsViewModelTests
{
	[TestMethod]
	[TestCategory("Constructor")]
	public void SettingsViewModelConstructorTest()
	{
		long id = long.MaxValue;
		string apiKey = Guid.NewGuid().ToString();
		Mock<INavigationService> navigationServiceMock = new();
		Mock<IOptions<SteamSettings>> optionsMock = new();
		_ = optionsMock.Setup(x => x.Value).Returns(new SteamSettings() { Id = id, ApiKey = apiKey });

		SettingsViewModel viewModel = new(optionsMock.Object, navigationServiceMock.Object);

		Assert.IsNotNull(viewModel);
		Assert.AreEqual(optionsMock.Object.Value, viewModel.Model);
		Assert.AreEqual(navigationServiceMock.Object, viewModel.NavigationService);
	}
}
