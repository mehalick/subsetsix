
var builder = DistributedApplication.CreateBuilder(args);

var dynamoDb = builder.AddAWSDynamoDBLocal("dynamodb");

var api = builder.AddProject<Projects.Subsetsix_Api>("api")
    .WithHttpsHealthCheck("/health")
    .WithReference(dynamoDb);

builder.AddProject<Projects.Subsetsix_Web>("web")
    .WithExternalHttpEndpoints()
    .WithHttpsHealthCheck("/health")
    .WithReference(api)
    .WaitFor(api);

builder.Build().Run();