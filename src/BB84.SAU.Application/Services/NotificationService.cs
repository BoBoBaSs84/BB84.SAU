using BB84.SAU.Application.Interfaces.Application.Services;

namespace BB84.SAU.Application.Services;

/// <summary>
/// The notification service class.
/// </summary>
internal sealed class NotificationService : INotificationService
{
	public event AsyncNotificationEventHandler? AsyncNotificationReceived;

	public event NotificationEventHandler? NotificationReceived;

	public void Send(string message)
	{
		if (NotificationReceived is not null)
			NotificationReceived.Invoke(this, message);
	}

	public async Task SendAsync(string message)
	{
		if (AsyncNotificationReceived is not null)
			await AsyncNotificationReceived.Invoke(this, message);
	}
}
