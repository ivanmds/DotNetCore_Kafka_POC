using Confluent.Kafka;
using Feed.Api.Model;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Feed.Api.Kafka.Producers
{
    public class CustomerProducer : IDisposable
    {
        private readonly ProducerBuilder<Null, string> _producerBuilder;
        private readonly IProducer<Null, string> _producer;
        protected readonly ProducerConfig _producerConfig;
        public static string TOPIC_NAME = "customer-topic";

        public CustomerProducer()
        {
            _producerConfig = new ProducerConfig {  BootstrapServers = "broker:29092" };
            _producerBuilder = new ProducerBuilder<Null, string>(_producerConfig);
            _producer = _producerBuilder.Build();
        }

        public async Task AddAsync(Customer customer)
        {
            if (customer == null) return;

            var value = JsonConvert.SerializeObject(customer);
            await _producer.ProduceAsync(TOPIC_NAME, new Message<Null, string> { Value = value });
        }

        public void Dispose()
        {
            if (_producer != null)
                _producer.Dispose();
        }
    }
}
