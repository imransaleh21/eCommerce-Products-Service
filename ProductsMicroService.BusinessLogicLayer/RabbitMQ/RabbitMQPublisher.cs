using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace ProductsMicroService.BusinessLogicLayer.RabbitMQ;

public class RabbitMQPublisher : IRabbitMQPublisher, IDisposable
{
    private readonly IConfiguration _configuration;
    private readonly IConnection _connection;
    private readonly IChannel _channel;

    public RabbitMQPublisher(IConfiguration configuration)
    {
        _configuration = configuration;
        var connectionFactory = new ConnectionFactory
        {
            HostName = _configuration["RABBITMQ_HOST"]!,
            Port = int.Parse(_configuration["RABBITMQ_PORT"]!),
            UserName = _configuration["RABBITMQ_USER"]!,
            Password = _configuration["RABBITMQ_PASS"]!
        };

        _connection = connectionFactory.CreateConnectionAsync().GetAwaiter().GetResult();
        _channel = _connection.CreateChannelAsync().GetAwaiter().GetResult();
    }

    public async Task PublishMessageAsync<T>(string routingKey, T message)
    {
        if (message == null) throw new ArgumentNullException(nameof(message), "Message cannot be null.");
        string jsonMsg = JsonSerializer.Serialize(message, message.GetType());
        byte[] body = Encoding.UTF8.GetBytes(jsonMsg);
        // Create exchange if it doesn't exist (but cxchange can be created at the project startup)
        string exchangeName = "products-exchange";
        await _channel.ExchangeDeclareAsync(
            exchange: exchangeName, 
            type: ExchangeType.Direct, 
            durable: true, 
            autoDelete: false
        );
        // Publish the message to the exchange with the specified routing key
        await _channel.BasicPublishAsync(
            exchange: exchangeName,
            routingKey: routingKey,
            body: body 
        );
    }

    public void Dispose()
    {
        _channel?.Dispose();
        _connection?.Dispose();
    }
}
