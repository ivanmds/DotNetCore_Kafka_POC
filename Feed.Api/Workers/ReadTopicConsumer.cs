using System;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;

namespace Feed.Api.Workers
{
    public class ReadTopicConsumer : BackgroundService
    {
        private readonly IConsumer<Ignore, string> _consumer;
        private string topicname = "topicfeed2";

        public ReadTopicConsumer(IConsumer<Ignore, string> consumer)
        {
            _consumer = consumer;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _consumer.Subscribe(topicname);
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _consumer?.Dispose();
            return base.StopAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //while (!stoppingToken.IsCancellationRequested)
            //{

            try
            {
                var cr = _consumer.Consume(stoppingToken);

                if (cr != null)
                    Console.WriteLine($"Consumed message '{cr.Value}' at: '{cr.TopicPartitionOffset}'.");
            }
            catch (OperationCanceledException)
            {
                // Ensure the consumer leaves the group cleanly and final offsets are committed.

                _consumer.Close();
            }
            //}
        }
    }
}
