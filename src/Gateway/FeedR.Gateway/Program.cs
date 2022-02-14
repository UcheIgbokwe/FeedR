var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection(key:"yarp"));
var app = builder.Build();

app.MapGet("/", () => "FeedR Gateway");

app.Run();