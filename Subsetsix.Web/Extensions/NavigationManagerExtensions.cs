using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;

namespace Subsetsix.Web.Extensions;

public static class NavigationManagerExtensions
{
    public static DateOnly TryGetDate(this NavigationManager navigationManager, string key)
    {
        var uri = navigationManager.ToAbsoluteUri(navigationManager.Uri);

        var defaultDate = DateOnly.FromDateTime(DateTime.Now);

        if (!QueryHelpers.ParseQuery(uri.Query).TryGetValue(key, out var d1))
        {
            return defaultDate;
        }

        return DateOnly.TryParse(d1, out var d2) ? d2 : defaultDate;
    }
}