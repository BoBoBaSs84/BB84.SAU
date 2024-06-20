using System.Diagnostics.CodeAnalysis;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

using BB84.SAU.Domain.Settings;

namespace BB84.SAU.Extensions;

/// <summary>
/// The host builder extensions class.
/// </summary>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here.")]
internal static class HostBuilderExtensions
{
	/// <summary>
	/// Adds the application settings to the host builder.
	/// </summary>
	/// <param name="host">The host builder instance to use.</param>
	/// <returns>The enriched host builder.</returns>
	public static IHostBuilder AddApplicationSettings(this IHostBuilder host)
	{
		host.ConfigureAppConfiguration((context, builder) =>
		{
			builder.SetBasePath(context.HostingEnvironment.ContentRootPath)
			.AddJsonFile("appsettings.json", true, true)
			.AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json", true, false)
			.AddEnvironmentVariables();

			if (context.HostingEnvironment.IsDevelopment())
				builder.AddUserSecrets<SteamSettings>(true, false);
		});

		return host;
	}
}
