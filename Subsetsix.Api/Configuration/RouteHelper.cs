using System.Runtime.CompilerServices;

namespace Subsetsix.Api.Configuration;

public class RouteHelper
{
    public static void Get([CallerFilePath] string callerFilePath = "")
    {

    }

    private static ReadOnlySpan<string> SplitPathSpan(ReadOnlySpan<char> path)
    {
        if (path.IsEmpty)
        {
            return [];
        }

        var fullPath = Path.GetFullPath(path.ToString());

        if (Path.IsPathRooted(fullPath))
        {
            fullPath = fullPath[Path.GetPathRoot(fullPath)!.Length..];
        }

        return fullPath.Split(
            [Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar],
            StringSplitOptions.RemoveEmptyEntries);
    }
}