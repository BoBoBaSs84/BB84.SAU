using BB84.SAU.Application.Services;

namespace BB84.SAU.Application.Tests.Services;

[TestClass]
public sealed class NotificationServiceTests
{
	[TestMethod]
	[TestCategory("Methods")]
	public void SendShouldRaiseNotificationReceivedEventHandler()
	{
		bool notificationReceived = false;
		NotificationService service = new();
		service.NotificationReceived += (s, e) => notificationReceived = true;

		service.Send(string.Empty);

		Assert.IsTrue(notificationReceived);
		Assert.IsTrue(service.Messages.Any());
	}

	[TestMethod]
	[TestCategory("Methods")]
	public async Task SendAsyncShouldNotRaiseAsyncNotificationReceivedEventHandler()
	{
		bool notificationReceived = false;
		NotificationService service = new();
		service.AsyncNotificationReceived += (s, e) => Task.Run(() => notificationReceived = true);

		await service.SendAsync(string.Empty)
			.ConfigureAwait(true);

		Assert.IsTrue(notificationReceived);
		Assert.IsTrue(service.Messages.Any());
	}
}
