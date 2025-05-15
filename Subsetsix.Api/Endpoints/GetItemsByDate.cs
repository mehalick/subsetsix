namespace Subsetsix.Api.Endpoints;

public class GetItemsByDate(IAmazonDynamoDB client) : Endpoint<GetItemsByDateRequest, GetItemsByDateResponse>
{
    public override void Configure()
    {
        Get(ApiRoutes.Items.GetItemsByDate);
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetItemsByDateRequest req, CancellationToken ct)
    {
        var queryRequest = new QueryRequest
        {
            TableName = Constants.ItemsTableName,
            IndexName = "UserId-Date-Index",
            KeyConditionExpression = "#userId = :userId and #date = :date",
            ExpressionAttributeNames = new()
            {
                {"#userId", "userId"},
                {"#date", "date"}
            },
            ExpressionAttributeValues = new()
            {
                {":userId", new(Constants.UserId)},
                {":date", new(req.Date.ToString("yyyy-MM-dd"))}
            }
        };

        var result = await client.QueryAsync(queryRequest, ct);

        var items = result.Items
            .Select(i => new GetItemsByDateResponseItem(i["itemId"].S, i["userId"].S, i["title"].S, i["tags"].SS))
            .ToList();

        await SendAsync(new(items), cancellation: ct);
    }
}
