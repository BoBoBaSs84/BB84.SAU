using BB84.Notifications.Extensions;
using BB84.SAU.Application.Interfaces.Application.Services;

namespace BB84.SAU.Application.Services;

/// <summary>
/// The notification service class.
/// </summary>
internal sealed class NotificationService : INotificationService
{
	/// <inheritdoc/>
	public event AsyncNotificationEventHandler? AsyncNotificationReceived;

	/// <inheritdoc/>
	public event NotificationEventHandler? NotificationReceived;

	/// <inheritdoc/>
	public void Send(string message)
	{
		AsyncNotificationReceived?.Invoke(this, message)
			.FireAndForgetSafeAsync();
		NotificationReceived?.Invoke(this, message);
	}
}
