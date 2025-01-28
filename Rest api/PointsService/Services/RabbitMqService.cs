using PointsService.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace PointsService.Services
{
    public static class RabbitMqService
    {
        private static readonly ConnectionFactory _factory = new() { HostName = "localhost" };
        private static IConnection? _connection;
        private static IChannel? _producerChannel;
        private static IChannel? _consumerChannel;

        public static async Task InitializeConnectionAsync()
        {
           
                _connection = await _factory.CreateConnectionAsync();

                _producerChannel = await _connection.CreateChannelAsync();
                await _producerChannel.QueueDeclareAsync(
                    queue: "pointsQueue",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                );

                // יצירת ערוץ לקבלת הודעות
                _consumerChannel = await _connection.CreateChannelAsync();
                await _consumerChannel.QueueDeclareAsync(
                    queue: "pointsQueue",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                );

                Console.WriteLine("RabbitMQ connection and channels initialized.");
            }

        public static async Task SendPointToRabbitAsync(PointRequest point)
        {
            if (_producerChannel == null || !_producerChannel.IsOpen)
            {
                Console.WriteLine("Error: Producer channel is not available!");
                return;
            }

            var jsonMessage = JsonSerializer.Serialize(point);
            var body = Encoding.UTF8.GetBytes(jsonMessage);

            var basicProperties = new BasicProperties();

            await _producerChannel.BasicPublishAsync<BasicProperties>(
                exchange: "",
                routingKey: "pointsQueue",
                mandatory: false,
                basicProperties: basicProperties,
                body: body,
                cancellationToken: CancellationToken.None
            );

            Console.WriteLine($" [x] Sent {jsonMessage} to RabbitMQ");
        }

        public static async Task StartRabbitMqListener(WebSocketHandler webSocketHandler)
        {
            if (_consumerChannel == null || !_consumerChannel.IsOpen)
            {
                Console.WriteLine("Error: Consumer channel is not available!");
                return;
            }

            var consumer = new AsyncEventingBasicConsumer(_consumerChannel);
            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var point = JsonSerializer.Deserialize<PointRequest>(message);

                if (point != null)
                {
                    Console.WriteLine($"[x] API Received back {point.Name} - X: {point.X}, Y: {point.Y}");
                    await webSocketHandler.BroadcastMessageAsync(message);
                }
            };

            await _consumerChannel.BasicConsumeAsync(queue: "pointsQueue", autoAck: true, consumer: consumer);

            Console.WriteLine("RabbitMQ async listener started.");
            await Task.Delay(Timeout.Infinite);
        }

    }
}
