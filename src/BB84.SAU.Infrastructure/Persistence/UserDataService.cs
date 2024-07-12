using System.Text.Json;
using System.Text.Json.Serialization;

using BB84.Extensions.Serialization;
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
/// <param name="directoryProvider">The directory provider instance to use.</param>
/// <param name="fileProvider">The file provider instance to use.</param>
internal sealed class UserDataService(ILoggerService<UserDataService> loggerService, IDirectoryProvider directoryProvider, IFileProvider fileProvider) : IUserDataService
{
	private static readonly Action<ILogger, Exception?> LogException =
		LoggerMessage.Define(LogLevel.Error, 0, "Exception occured.");

	private static readonly JsonSerializerOptions SerializerOptions = new(JsonSerializerDefaults.Web)
	{
		DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
	};
	private static readonly string AppName = "BB84.SAU";
	private static readonly string DataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
	private static readonly string DataFile = "UserData.json";

	public async Task<UserDataModel> LoadUserDataAsync(CancellationToken cancellationToken = default)
	{
		try
		{
			string filePath = Path.Combine(DataFolder, AppName, DataFile);

			if (!fileProvider.Exists(filePath))
				return new();

			string fileContent = await fileProvider.ReadAllTextAsync(filePath, cancellationToken);

			UserDataModel userData = fileContent.FromJson<UserDataModel>(SerializerOptions);

			return userData;
		}
		catch (Exception ex)
		{
			loggerService.Log(LogException, ex);
			return new();
		}
	}

	public async Task<bool> SaveUserDataAsync(UserDataModel userData, CancellationToken cancellationToken = default)
	{
		try
		{
			_ = directoryProvider.CreateDirectory(Path.Combine(DataFolder, AppName));

			string filePath = Path.Combine(DataFolder, AppName, DataFile);

			string fileContent = userData.ToJson(SerializerOptions);

			await fileProvider.WriteAllTextAsync(filePath, fileContent, cancellationToken);

			return true;
		}
		catch (Exception ex)
		{
			loggerService.Log(LogException, ex);
			return false;
		}
	}
}
