using PointsService.Services;
using PointsService.Models;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<WebSocketHandler>();
var app = builder.Build();
await RabbitMqService.InitializeConnectionAsync();

app.MapPost("/points", async (PointRequest request) =>
{
    Console.WriteLine($"Received point: Name={request.Name}, X={request.X}, Y={request.Y}");
    await RabbitMqService.SendPointToRabbitAsync(request);
    return Results.Ok("Point received and updated in RabbitMQ!");
});

app.UseWebSockets();
app.Map("/ws", async context =>
{
    if (context.WebSockets.IsWebSocketRequest)
    {
        var webSocketHandler = context.RequestServices.GetRequiredService<WebSocketHandler>();
        await webSocketHandler.HandleConnectionAsync(await context.WebSockets.AcceptWebSocketAsync());
    }
    else
    {
        context.Response.StatusCode = 400;
    }
});

_ = Task.Run(() => RabbitMqService.StartRabbitMqListener(app.Services.GetRequiredService<WebSocketHandler>()));
app.Run();
