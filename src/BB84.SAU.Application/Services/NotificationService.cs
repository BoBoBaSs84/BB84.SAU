// -----------------------------------------------------------------------------
// Copyright:	Robert Peter Meyer
// License:		MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------------
using System.Collections.ObjectModel;

using BB84.SAU.Application.Interfaces.Application.Services;
using BB84.SAU.Domain.Models;

namespace BB84.SAU.Application.Services;

/// <summary>
/// The notification service class.
/// </summary>
internal sealed class NotificationService : INotificationService
{
	public event AsyncNotificationEventHandler? AsyncNotificationReceived;

	public event NotificationEventHandler? NotificationReceived;

	public ObservableCollection<LogbookModel> Messages { get; } = [];

	public void Send(string message)
	{
		Messages.Add(new(message));
		NotificationReceived?.Invoke(this, message);
	}

	public async Task SendAsync(string message)
	{
		Messages.Add(new(message));
		await AsyncNotificationReceived?.Invoke(this, message)!;
	}
}
