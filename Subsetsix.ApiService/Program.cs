global using Amazon.DynamoDBv2;
global using FastEndpoints;
global using Subsetsix.ApiService.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddFastEndpoints();
builder.Services.AddProblemDetails();
builder.Services.AddOpenApi();

var options = builder.Configuration.GetAWSOptions();
builder.Services.AddDefaultAWSOptions(options);
builder.Services.AddAWSService<IAmazonDynamoDB>();

var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    await app.Services.TryCreateTable();
}

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseFastEndpoints();

string[] summaries = ["Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"];

app.MapGet("/weatherforecast", () =>
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
            .ToArray();
        return forecast;
    })
    .WithName("GetWeatherForecast");

app.MapDefaultEndpoints();

await app.RunAsync();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int) (TemperatureC / 0.5556);
}

record Item(string ItemId, string UserId)
{
    public string Date { get; init; } = "";
    public string Title { get; init; } = "";
    public string Description { get; init; } = "";
    public string Tags { get; init; } = "";

}