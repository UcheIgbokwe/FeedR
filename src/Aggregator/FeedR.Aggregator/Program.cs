var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
var id = Guid.NewGuid().ToString(format:"N");

app.MapGet("/", () => $"FeedR Aggregator {id}");

app.Run();