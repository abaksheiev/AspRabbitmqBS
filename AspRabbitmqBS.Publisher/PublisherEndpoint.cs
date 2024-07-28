using AspRabbitmqBS.Publisher.Models;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;

namespace AspRabbitmqBS.Publisher;

public static class PublisherEndpointExtension
{
    public static void RegisterPublisherEndpoint(this WebApplication app) {
        MapEndpoint(app);
    }

    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/publishMessage", PublisherResult)
              .WithTags("AuthorsApi")
              .WithOpenApi();
    }


    public static IResult PublisherResult(
           string text,
           IOptions<RabbitMQSettings> settings,
           CancellationToken cancellationToken)
    {
        var factory = new ConnectionFactory { HostName = settings.Value.HostName };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(queue: settings.Value.QueueName,
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

        var body = Encoding.UTF8.GetBytes(text);

        channel.BasicPublish(exchange: string.Empty,
                     routingKey: settings.Value.QueueName,
                     basicProperties: null,
                     body: body);

        return Results.Accepted();
    }
}
