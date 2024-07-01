using BB84.SAU.Domain.Models;

namespace BB84.SAU.Application.Interfaces.Infrastructure.Persistence;

/// <summary>
/// The user data service interface.
/// </summary>
public interface IUserDataService
{
	/// <summary>
	/// Loads the persisted state of the user data.
	/// </summary>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns><see cref="Task{TResult}"/></returns>
	Task<UserDataModel> LoadUserDataAsync(CancellationToken cancellationToken = default);

	/// <summary>
	/// Saves the current user data state.
	/// </summary>
	/// <param name="userData">The user data to persist.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns><see cref="Task{TResult}"/></returns>
	Task<bool> SaveUserDataAsync(UserDataModel userData, CancellationToken cancellationToken = default);
}
