
var builder = DistributedApplication.CreateBuilder(args);

var dynamoDb = builder.AddAWSDynamoDBLocal("dynamodb");

var apiService = builder.AddProject<Projects.Subsetsix_ApiService>("api")
    .WithHttpsHealthCheck("/health")
    .WithReference(dynamoDb);

builder.AddProject<Projects.Subsetsix_Web>("web")
    .WithExternalHttpEndpoints()
    .WithHttpsHealthCheck("/health")
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();