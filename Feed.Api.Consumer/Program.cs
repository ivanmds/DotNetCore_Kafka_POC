using Confluent.Kafka;
using System;
using System.Threading;

namespace Feed.Api.Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            string kafkaEndpoint = "localhost:9092";

            var conf = new ConsumerConfig
            {
                GroupId = "test-consumer-group",
                BootstrapServers = kafkaEndpoint,
                AutoOffsetReset = AutoOffsetReset.Latest
            };

            using (var c = new ConsumerBuilder<Ignore, string>(conf).Build())
            {
                c.Subscribe("topicfeed2");

                CancellationTokenSource cts = new CancellationTokenSource();
                Console.CancelKeyPress += (_, e) => {
                    e.Cancel = true;
                    cts.Cancel(); 
                };

                try
                {
                    while (true)
                    {
                        try
                        {
                            var cr = c.Consume(cts.Token);
                            Console.WriteLine($"Consumed message '{cr.Value}' at: '{cr.TopicPartitionOffset}'.");
                        }
                        catch (ConsumeException e)
                        {
                            throw e;
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    // Ensure the consumer leaves the group cleanly and final offsets are committed.
                    c.Close();
                }
            }
        }
    }
}
