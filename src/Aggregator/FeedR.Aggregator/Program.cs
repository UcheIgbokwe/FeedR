using FeedR.Shared.Redis;
using FeedR.Shared.Redis.Streaming;
using FeedR.Shared.Serialization;
using FeedR.Shared.Streaming;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddStreaming()
    .AddSerialization()
    .AddRedis(builder.Configuration)
    .AddRedisStreaming();

var app = builder.Build();
var id = Guid.NewGuid().ToString(format:"N");

app.MapGet("/", () => $"FeedR Aggregator {id}");

app.Run();