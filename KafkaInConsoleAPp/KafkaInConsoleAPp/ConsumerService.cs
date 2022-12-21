using Kafka.Public;
using Kafka.Public.Loggers;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafkaInConsoleApp
{
    internal class ConsumerService : IHostedService
    {
        private readonly ILogger<ConsumerService> _logger;
        private readonly ClusterClient _cluster;

        public ConsumerService(ILogger<ConsumerService> logger)
        {
            _logger = logger;
            _cluster = new ClusterClient(new Configuration
            {
                Seeds = "localhost:9092"
            }, new ConsoleLogger());
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _cluster.ConsumeFromLatest("demo");
            _cluster.MessageReceived += record => _logger.LogInformation("Received: " + Encoding.UTF8.GetString(record.Value as byte[]));
            return Task.CompletedTask;
        }

        private void _cluster_MessageReceived(RawKafkaRecord obj)
        {
            _logger.LogInformation("Received: " + obj.Value.ToString());
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _cluster.Dispose();
            return Task.CompletedTask;
        }
    }
}
