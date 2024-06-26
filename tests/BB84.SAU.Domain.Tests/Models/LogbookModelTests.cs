using BB84.SAU.Domain.Models;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BB84.SAU.Domain.Tests.Models;

[TestClass]
public sealed class LogbookModelTests
{
	[TestMethod]
	[TestCategory("Constructor")]
	public void LogbookModelTest()
	{
		LogbookModel? model;
		string message = "Unit Test Message";

		model = new LogbookModel(message);

		Assert.IsNotNull(model);
		Assert.AreEqual(message, model.Message);
		Assert.AreNotEqual(Guid.Empty, model.Id);
		Assert.AreNotEqual(DateTime.MinValue, model.DateTimeUtc);
	}
}
