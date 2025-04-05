// -----------------------------------------------------------------------------
// Copyright:	Robert Peter Meyer
// License:		MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------------
using BB84.SAU.Application.Services;
using BB84.SAU.Application.ViewModels;

namespace BB84.SAU.Application.Tests.ViewModels;

[TestClass]
public sealed class LogbookViewModelTests
{
	[TestMethod]
	public void LogbookViewModelNotificationTest()
	{
		string message = "Unit Test Message";
		NotificationService notificationService = new();
		LogbookViewModel viewModel = new(notificationService);

		notificationService.Send(message);

		Assert.AreEqual(message, viewModel.LogbookEntries.First().Message);
	}
}
