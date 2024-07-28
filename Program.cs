using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace AspRabbitmqBS;

class Program
{
     public static void Main(string[] args)
  {
      var host = CreateHostBuilder(args).Build();
      host.Run();
  }
    public static IHostBuilder CreateHostBuilder(string[] args) =>
      Host.CreateDefaultBuilder(args)
          .ConfigureServices((hostContext, services) =>
          {

          })
          .ConfigureLogging(logging =>
           {
               logging.ClearProviders();
               logging.AddConsole();
           })
          
       ;
}
