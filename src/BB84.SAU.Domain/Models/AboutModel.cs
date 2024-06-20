using System.Diagnostics;
using System.Reflection;
using System.Runtime.Versioning;

using BB84.SAU.Domain.Models.Base;

namespace BB84.SAU.Domain.Models;

/// <summary>
/// The about model.
/// </summary>
public sealed class AboutModel : ModelBase
{
	/// <summary>
	/// Initializes an instance of <see cref="AboutModel"/> class.
	/// </summary>
	public AboutModel()
	{
		Assembly assembly = Assembly.GetExecutingAssembly();
		FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);

		Title = fileVersionInfo.ProductName;
		Version = fileVersionInfo.FileVersion;
		Comments = fileVersionInfo.Comments;
		Company = fileVersionInfo.CompanyName;
		Copyright = fileVersionInfo.LegalCopyright;
		FrameworkName = GetFrameworkName(assembly);
		Repository = GetRepositoryLocation(assembly);
	}

	/// <summary>
	/// The title of the application.
	/// </summary>
	public string? Title { get; }

	/// <summary>
	/// The version of the application.
	/// </summary>
	public string? Version { get; }

	/// <summary>
	/// The comments of the application.
	/// </summary>
	public string? Comments { get; }

	/// <summary>
	/// The company of the application.
	/// </summary>
	public string? Company { get; }

	/// <summary>
	/// The copyright of the application.
	/// </summary>
	public string? Copyright { get; }

	/// <summary>
	/// The name of the .NET version with which the assembly was compiled.
	/// </summary>
	public string? FrameworkName { get; }

	/// <summary>
	/// The repository location of the application.
	/// </summary>
	public string? Repository { get; }

	private static string? GetFrameworkName(Assembly assembly)
	{
		TargetFrameworkAttribute? targetFramework = assembly
			.GetCustomAttributes(typeof(TargetFrameworkAttribute), false)
			.SingleOrDefault() as TargetFrameworkAttribute;

		return targetFramework is not null ? targetFramework.FrameworkName : default;
	}

	private static string? GetRepositoryLocation(Assembly assembly)
	{
		IEnumerable<AssemblyMetadataAttribute>? assemblyMetadata = assembly
			.GetCustomAttributes(typeof(AssemblyMetadataAttribute), false) as IEnumerable<AssemblyMetadataAttribute>;

		return assemblyMetadata?.Where(x => x.Key == "RepositoryUrl").Select(x => x.Value).SingleOrDefault();
	}
}
