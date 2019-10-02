using FeedService.Model;
using System.Threading.Tasks;

namespace FeedService.Kafka.Producers
{
    public interface IFeedEventProducer : IProducerBase
    {
        Task PublishAsync(FeedEvent feedEvent);
    }
}
