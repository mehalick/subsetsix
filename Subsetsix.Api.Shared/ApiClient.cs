using Subsetsix.Api.Shared.Endpoints.Items;
using System.Net.Http.Json;

namespace Subsetsix.Api.Shared;

public class ApiClient(HttpClient httpClient)
{
    public async Task<GetItemsByDateResponseItem[]> GetItemsByDate(DateOnly date, CancellationToken cancellationToken = default)
    {
        var url = ApiRoutes.Items.GetItemsByDateWithQuery(date);
        var response = await httpClient.GetFromJsonAsync<GetItemsByDateResponse>(url, cancellationToken);

        return response?.Items.ToArray() ?? [];
    }
}
