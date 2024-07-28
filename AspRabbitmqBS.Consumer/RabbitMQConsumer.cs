using AspRabbitmqBS.Consumer.Models;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System.Text;

namespace AspRabbitmqBS.Consumer;

public class RabbitMQConsumer
{
    private readonly RabbitMQSettings _settings;
    private IConnection _connection;
    private IModel _channel;

    public RabbitMQConsumer(IOptions<RabbitMQSettings> options)
    {
        _settings = options.Value;
    }

    public void Connect(CancellationToken stoppingToken)
    {
        var factory = new ConnectionFactory()
        {
            HostName = _settings.HostName,
            UserName = _settings.UserName,
            Password = _settings.Password
        };

        while (!stoppingToken.IsCancellationRequested)
        {
            Console.WriteLine("RabbitMQ is Connecting...");

            try
            {
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();
                EnsureQueueExists();
                Console.WriteLine("RabbitMQ connection established.");
                break;
            }
            catch (BrokerUnreachableException)
            {
                Console.WriteLine("RabbitMQ is not available yet. Retrying in 5 seconds...");
                Task.Delay(5000).Wait();
            }
        }
    }

    public void StartConsuming(CancellationToken stoppingToken)
    {
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine(" [x] Received {0}", message);
        };

        _channel.BasicConsume(queue: _settings.QueueName,
                              autoAck: true,
                              consumer: consumer);

        stoppingToken.Register(() =>
        {
            _channel.Close();
            _connection.Close();
        });
    }

    private void EnsureQueueExists()
    {
        _channel.QueueDeclare(queue: _settings.QueueName,
                              durable: false,
                              exclusive: false,
                              autoDelete: false,
                              arguments: null);
    }
}

