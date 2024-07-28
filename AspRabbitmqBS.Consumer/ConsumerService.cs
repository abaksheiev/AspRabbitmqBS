using Microsoft.Extensions.Hosting;

namespace AspRabbitmqBS.Consumer;

public class ConsumerService : BackgroundService
{
    private readonly RabbitMQConsumer _rabbitMQConsumer;

    public ConsumerService(RabbitMQConsumer rabbitMQConsumer)
    {
        _rabbitMQConsumer = rabbitMQConsumer;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _rabbitMQConsumer.Connect(stoppingToken);
        _rabbitMQConsumer.StartConsuming(stoppingToken);
    }
}