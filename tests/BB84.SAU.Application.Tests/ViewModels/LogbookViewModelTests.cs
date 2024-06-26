using BB84.SAU.Application.Interfaces.Application.Services;
using BB84.SAU.Application.ViewModels;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace BB84.SAU.Application.Tests.ViewModels;

[TestClass]
public sealed class LogbookViewModelTests
{
	[TestMethod]
	public void LogbookViewModelTest()
	{
		Mock<INotificationService> notificationServiceMock = new();

		LogbookViewModel viewModel = new(notificationServiceMock.Object);

		Assert.IsNotNull(viewModel);
		Assert.AreEqual(default, viewModel.LogbookEntries.Count);
	}
}
