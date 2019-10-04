using Confluent.Kafka;

namespace Feed.Api.Workers
{
    public class ConsumerWrapper
    {
        private string _topicName;
        private ConsumerConfig _consumerConfig;
        private IConsumer<string, string> _consumer;

        public ConsumerWrapper(ConsumerConfig config, string topicName)
        {
            this._topicName = topicName;
            this._consumerConfig = config;
            this._consumer = new ConsumerBuilder<string, string>(this._consumerConfig).Build();
            this._consumer.Subscribe(_topicName);
        }
        public string ReadMessage()
        {
            var consumeResult = this._consumer.Consume();
            return consumeResult.Value;
        }
    }
}
