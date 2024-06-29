using BB84.SAU.Application.Services;

namespace BB84.SAU.Application.Tests.Services;

[TestClass]
public sealed class NotificationServiceTests
{
	[TestMethod]
	public void SendTest()
	{
		bool notificationReceived = false;
		NotificationService service = new();
		service.NotificationReceived += (s, e) => notificationReceived = true;

		service.Send("Unit test message.");

		Assert.IsTrue(notificationReceived);
	}
}
