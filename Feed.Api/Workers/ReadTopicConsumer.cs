using System;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;

namespace Feed.Api.Workers
{
    public class ReadTopicConsumer : BackgroundService
    {
        private readonly ConsumerConfig _consumerConfig;

        public ReadTopicConsumer(ConsumerConfig consumerConfig)
        {
            _consumerConfig = consumerConfig;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var conf = new ConsumerConfig
            {
                GroupId = "test-consumer-group",
                BootstrapServers = "broker:29092",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using var c = new ConsumerBuilder<Ignore, string>(conf).Build();

            c.Subscribe("topicfeed2");

            try
            {
                var cr = c.Consume(TimeSpan.FromSeconds(1));

                if(cr != null)
                    Console.WriteLine($"Consumed message '{cr.Value}' at: '{cr.TopicPartitionOffset}'.");
            }
            catch (OperationCanceledException)
            {
                // Ensure the consumer leaves the group cleanly and final offsets are committed.
                c.Close();
            }
        }
    }
}
