namespace FeedService.Kafka.Producers
{
    public interface IProducerBase
    {
        string TopicName { get; }
    }
}
