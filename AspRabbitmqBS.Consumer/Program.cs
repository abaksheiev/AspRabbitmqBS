using AspRabbitmqBS.Consumer.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AspRabbitmqBS.Consumer;

public class Program
{
    public static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();
    
        host.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, config) =>
             {
                 config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                 config.AddEnvironmentVariables();
             })
            .ConfigureServices((hostContext, services) =>
            {
                services.Configure<RabbitMQSettings>(hostContext.Configuration.GetSection("RabbitMQSettings"));
                services.AddSingleton<RabbitMQConsumer>();
                services.AddHostedService<ConsumerService>();
            })
         ;
}