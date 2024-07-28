using Microsoft.AspNetCore.Mvc;

namespace AspRabbitmqBS.Publisher;

public static class PublisherEndpointExtension
{
    public static void RegisterPublisherEndpoint(this WebApplication app)
    {
        MapEndpoint(app);
    }

    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app
            .MapPost("/publishMessage", PublisherResult)
            .WithOpenApi();
    }

    public static IResult PublisherResult(
           string text,
           [FromServices] RabbitMQService rabbitMQService)
    {
        rabbitMQService.PublishMessage(text);
        return Results.Accepted();
    }
}
