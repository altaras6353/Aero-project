using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text;
// using System.Threading.Tasks;

namespace PointsService.Services
{
    public class WebSocketHandler
    {
        private readonly ConcurrentBag<WebSocket> _sockets = new();

        public async Task HandleConnectionAsync(WebSocket webSocket)
        {
            _sockets.Add(webSocket);
            Console.WriteLine("WebSocket client connected");

            var buffer = new byte[1024 * 4];
            while (webSocket.State == WebSocketState.Open)
            {
                var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                if (result.CloseStatus.HasValue)
                {
                    _sockets.TryTake(out _);
                    await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
                    Console.WriteLine("WebSocket client disconnected");
                }
            }
        }

        public async Task BroadcastMessageAsync(string message)
        {
            Console.WriteLine($"Broadcasting message: {message}");
            var tasks = _sockets.Select(async socket =>
            {
                if (socket.State == WebSocketState.Open)
                {
                    var buffer = Encoding.UTF8.GetBytes(message);
                    await socket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
                    Console.WriteLine("Message sent to WebSocket client");
                }
                else
                {
                    Console.WriteLine("WebSocket client is not open");
                }
            });

            await Task.WhenAll(tasks);
        }
    }
}