// -----------------------------------------------------------------------------
// Copyright:	Robert Peter Meyer
// License:		MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------------
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

using BB84.SAU.Domain.Models;

namespace BB84.SAU.Application.Interfaces.Application.Services;

/// <summary>
/// The NotificationService interface.
/// </summary>
public interface INotificationService
{
	/// <summary>
	/// The event handler for asynchronous message receiving.
	/// </summary>
	event AsyncNotificationEventHandler? AsyncNotificationReceived;

	/// <summary>
	/// The event handler for synchronous message receiving.
	/// </summary>
	event NotificationEventHandler? NotificationReceived;

	/// <summary>
	/// Holds the sent messages.
	/// </summary>
	ObservableCollection<LogbookModel> Messages { get; }

	/// <summary>
	/// Sends a synchronous <paramref name="message"/> to all the subscribers.
	/// </summary>
	/// <param name="message">The message to send.</param>
	void Send(string message);

	/// <summary>
	/// Sends a asynchronous <paramref name="message"/> to all the subscribers.
	/// </summary>
	/// <param name="message">The message to send.</param>
	/// <returns><see cref="Task"/></returns>
	Task SendAsync(string message);
}

/// <summary>
/// Represents the method that will handle an asynchronous notification event when the event provides message data.
/// </summary>
/// <param name="sender">The source of the event.</param>
/// <param name="message">An <see cref="string"/> that contains the event message.</param>
/// <returns><see cref="Task"/></returns>
[SuppressMessage("Naming", "CA1711:Identifiers should not have incorrect suffix", Justification = "I want it that way.")]
public delegate Task AsyncNotificationEventHandler(object sender, string message);

/// <summary>
/// Represents the method that will handle an synchronous notification event when the event provides message data.
/// </summary>
/// <param name="sender">The source of the event.</param>
/// <param name="message">An <see cref="string"/> that contains the event message.</param>
[SuppressMessage("Naming", "CA1711:Identifiers should not have incorrect suffix", Justification = "I want it that way.")]
public delegate void NotificationEventHandler(object sender, string message);
