using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using FastEndpoints;

namespace Subsetsix.Api.Endpoints;

// ReSharper disable once ClassNeverInstantiated.Global
public record GetItemByIdRequest(string ItemId);
public record GetItemByIdResponse(
    string ItemId,
    string UserId,
    string Title,
    string Description,
    List<string> Tags);

public class GetItemById(IAmazonDynamoDB client) : Endpoint<GetItemByIdRequest, GetItemByIdResponse>
{
    public override void Configure()
    {
        Get("/items/getItemById");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetItemByIdRequest req, CancellationToken ct)
    {
        var key = new Dictionary<string, AttributeValue>
        {
            ["itemId"] = new AttributeValue(req.ItemId),
            ["userId"] = new AttributeValue(Constants.UserId)
        };

        var request = new GetItemRequest
        {
            TableName = Constants.ItemsTableName,
            Key = key
        };

        var response = await client.GetItemAsync(request, ct);

        if (response?.Item is null)
        {
            await SendNotFoundAsync(ct);
        }

        var r = new GetItemByIdResponse(
            response.Item["itemId"].S,
            response.Item["userId"].S,
            response.Item["title"].S,
            response.Item["description"].S,
            response.Item["tags"].SS
        );

        await SendAsync(r, cancellation: ct);
    }
}
