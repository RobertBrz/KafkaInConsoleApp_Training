using KafkaInConsoleApp;
using KafkaInConsoleAPp;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

internal class Program
{
    private static void Main(string[] args)
    {
        CreateBuilder(args).Build().Run();
    }
    private static IHostBuilder CreateBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
        .ConfigureServices((collection =>
        {
            collection.AddHostedService<ConsumerService>(); 
            collection.AddHostedService<ProducerService>();
        }
    ));
}