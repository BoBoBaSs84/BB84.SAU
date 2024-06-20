using BB84.SAU.Application.Interfaces.Application.Services;

namespace BB84.SAU.Application.Services;

/// <summary>
/// The notification service class.
/// </summary>
internal sealed class NotificationService : INotificationService
{
	public event AsyncNotificationEventHandler? MessageReceived;

	public void Send(string message)
		=> MessageReceived?.Invoke(this, message);
}
