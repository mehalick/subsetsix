using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;

namespace Subsetsix.ApiService.Configuration;

public static class ServiceExtensions
{
    public static async Task TryCreateTable(this IServiceProvider services)
    {
        var client = services.GetRequiredService<IAmazonDynamoDB>();
        var log = services.GetRequiredService<ILogger<Program>>();

        try
        {
            await client.DescribeTableAsync(Constants.ItemsTableName);
        }
        catch (ResourceNotFoundException)
        {
            await client.CreateTableAsync(new()
            {
                TableName = Constants.ItemsTableName,
                BillingMode = BillingMode.PAY_PER_REQUEST,
                KeySchema =
                [
                    new KeySchemaElement
                    {
                        AttributeName = "itemId",
                        KeyType = KeyType.HASH,
                    },
                    new KeySchemaElement
                    {
                        AttributeName = "userId",
                        KeyType = KeyType.RANGE
                    }
                ],
                AttributeDefinitions =
                [
                    new AttributeDefinition("itemId", ScalarAttributeType.S),
                    new AttributeDefinition("userId", ScalarAttributeType.S),
                    new AttributeDefinition("date", ScalarAttributeType.S),
                ],
                GlobalSecondaryIndexes = [
                    new GlobalSecondaryIndex
                    {
                        IndexName = "UserId-Date-Index",
                        KeySchema =
                        [
                            new KeySchemaElement
                            {
                                AttributeName = "userId",
                                KeyType = KeyType.HASH,
                            },
                            new KeySchemaElement
                            {
                                AttributeName = "date",
                                KeyType = KeyType.RANGE
                            }
                        ],
                        Projection = new()
                        {
                            ProjectionType = ProjectionType.INCLUDE,
                            NonKeyAttributes = ["title", "tags"]
                        }
                    }
                ]
            });

            for (var i = 0; i < 10; i++)
            {
                var itemId = Guid.CreateVersion7().ToString("N");

                await client.PutItemAsync(new()
                {
                    TableName = Constants.ItemsTableName,
                    Item = new()
                    {
                        ["itemId"] = new(itemId),
                        ["userId"] = new(Constants.UserId),
                        ["date"] = new(Constants.EmptyDate),
                        ["title"] = new($"Item {i}"),
                        ["description"] = new($"Description {i}"),
                        ["tags"] = new()
                        {
                            SS = ["system/pinned", "user/project-1"]
                        }
                    }
                });

                log.LogInformation("Created item {ItemId}", itemId);

                for (var j = 1; j <= 5; j++)
                {
                    var date = $"2025-05-0{i}";

                    await client.PutItemAsync(new()
                    {
                        TableName = Constants.ItemsTableName,
                        Item = new()
                        {
                            ["itemId"] = new($"{Guid.CreateVersion7():N}|{date}"),
                            ["userId"] = new(Constants.UserId),
                            ["date"] = new(date),
                            ["title"] = new($"Item {i}"),
                            ["tags"] = new()
                            {
                                SS = ["system/pinned", "user/project-1"]
                            }
                        }
                    });
                }
            }
        }
    }
}
