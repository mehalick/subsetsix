using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using System.Text;

namespace Subsetsix.Api.Configuration;

/// <summary>
/// Specifies an attribute route on an endpoint with an automatic route template.
/// </summary>
public class AutoRouteAttribute : RouteAttribute
{
	public AutoRouteAttribute([CallerFilePath] string callerFilePath = "") : base(GetRouteTemplate(callerFilePath))
	{
	}

	private static string GetRouteTemplate(string filePath)
	{
		var sb = new StringBuilder();
		var parts = GetEndpointParts(filePath);

		for (var i = 0; i < parts.Length; i++)
		{
			sb.Append(LowerPascalCase(parts[i]));

			if (i != parts.Length - 1)
			{
				sb.Append('.');
			}
		}

		return StripFileExtension(sb.ToString());
	}

	private static string[] GetEndpointParts(string filePath)
	{
		return filePath[0] switch
		{
			'/' => SplitEndpointParts(filePath, "/"), // Linux path
			_ => SplitEndpointParts(filePath, "\\")
		};
	}

	private static string[] SplitEndpointParts(string filePath, string pathSeparator)
	{
		var folder = "Endpoints" + pathSeparator;
		var index = filePath.IndexOf(folder, StringComparison.InvariantCulture);

		return filePath[(index + folder.Length)..].Split([pathSeparator], StringSplitOptions.None);
	}

	private static string LowerPascalCase(string s)
	{
		return s[0].ToString().ToLower() + s[1..];
	}

	private static string StripFileExtension(string s)
	{
		return s[..^3];
	}
}
