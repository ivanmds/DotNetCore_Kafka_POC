using Confluent.Kafka;
using FeedService.Model;
using System.Threading.Tasks;

namespace FeedService.Kafka.Producers
{
    public class FeedEventProducer : IFeedEventProducer
    {
        public string TopicName => "feed";

        public async Task PublishAsync(FeedEvent feedEvent)
        {
            var config = new ProducerConfig { BootstrapServers = KafkaConfig.UrlBootstrapServers };

            using var p = new ProducerBuilder<Null, FeedEvent>(config).Build();
            await p.ProduceAsync(TopicName, new Message<Null, FeedEvent> { Value = feedEvent });
        }
    }
}
