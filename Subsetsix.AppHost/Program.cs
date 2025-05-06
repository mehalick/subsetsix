var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.Subsetsix_ApiService>("apiservice")
    .WithHttpsHealthCheck("/health");

builder.AddProject<Projects.Subsetsix_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithHttpsHealthCheck("/health")
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();