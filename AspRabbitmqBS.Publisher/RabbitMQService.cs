using AspRabbitmqBS.Publisher.Models;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;

namespace AspRabbitmqBS.Publisher;

public class RabbitMQService
{
    private readonly IOptions<RabbitMQSettings> _settings;
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public RabbitMQService(IOptions<RabbitMQSettings> settings)
    {
        _settings = settings;

        var factory = new ConnectionFactory { HostName = _settings.Value.HostName };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        _channel.QueueDeclare(queue: _settings.Value.QueueName,
                              durable: false,
                              exclusive: false,
                              autoDelete: false,
                              arguments: null);
    }

    public void PublishMessage(string message)
    {
        var body = Encoding.UTF8.GetBytes(message);

        _channel.BasicPublish(exchange: string.Empty,
                              routingKey: _settings.Value.QueueName,
                              basicProperties: null,
                              body: body);
    }

    public void Dispose()
    {
        _channel?.Close();
        _connection?.Close();
    }
}