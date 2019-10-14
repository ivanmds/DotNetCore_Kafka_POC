using Confluent.Kafka;
using Feed.Api.Model;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Feed.Api.Kafka.Producers
{
    public class CustomerProducer : IDisposable
    {
        private readonly ProducerBuilder<string, string> _producerBuilder;
        private readonly IProducer<string, string> _producer;
        protected readonly ProducerConfig _producerConfig;
        private static string TOPIC_NAME = "customer_topic";

        public CustomerProducer()
        {
            _producerConfig = new ProducerConfig {  BootstrapServers = "broker:29092", Acks = Acks.All };
            _producerBuilder = new ProducerBuilder<string, string>(_producerConfig);
            _producer = _producerBuilder.Build();
        }

        public async Task AddAsync(Customer customer)
        {
            if (customer == null) return;

            var value = JsonConvert.SerializeObject(customer);
            await _producer.ProduceAsync(TOPIC_NAME, new Message<string, string> { Key = customer.Document, Value = value });
        }

        public void Dispose()
        {
            if (_producer != null)
                _producer.Dispose();
        }
    }
}
