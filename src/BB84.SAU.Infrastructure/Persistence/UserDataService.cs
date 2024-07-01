using BB84.Extensions.Serialization;
using BB84.SAU.Application.Interfaces.Application.Services;
using BB84.SAU.Application.Interfaces.Infrastructure.Persistence;
using BB84.SAU.Application.Interfaces.Infrastructure.Services;
using BB84.SAU.Domain.Models;
using BB84.SAU.Infrastructure.Interfaces.Provider;

using Microsoft.Extensions.Logging;

namespace BB84.SAU.Infrastructure.Persistence;

/// <summary>
/// The user data service class.
/// </summary>
/// <param name="loggerService">The logger service instance to use.</param>
/// <param name="notificationService">The notification service instance to use.</param>
/// <param name="fileProvider">The file provider instance to use.</param>
internal sealed class UserDataService(ILoggerService<UserDataService> loggerService, INotificationService notificationService, IFileProvider fileProvider) : IUserDataService
{
	private static readonly Action<ILogger, Exception?> LogException =
		LoggerMessage.Define(LogLevel.Error, 0, "Exception occured.");

	private static readonly string DataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
	private static readonly string DataFile = "UserData.json";

	public async Task<UserDataModel> LoadUserDataAsync(CancellationToken cancellationToken = default)
	{
		try
		{
			string filePath = Path.Combine(DataFolder, DataFile);

			string fileContent = await fileProvider.ReadAllTextAsync(filePath, cancellationToken);

			UserDataModel userData = fileContent.FromJson<UserDataModel>();

			string message = "User data successfully loaded.";

			await notificationService.SendAsync(message);

			return userData;
		}
		catch (Exception ex)
		{
			loggerService.Log(LogException, ex);
			notificationService.Send(ex.Message);
			return new();
		}
	}

	public async Task SaveUserDataAsync(UserDataModel userData, CancellationToken cancellationToken = default)
	{
		try
		{
			string filePath = Path.Combine(DataFolder, DataFile);

			string fileContent = userData.ToJson();

			await fileProvider.WriteAllTextAsync(filePath, fileContent, cancellationToken);

			string message = "User data successfully saved.";

			await notificationService.SendAsync(message);
		}
		catch (Exception ex)
		{
			loggerService.Log(LogException, ex);
			notificationService.Send(ex.Message);
		}
	}
}
