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

            Console.WriteLine("OrderProcessing Service Started");

            while (!stoppingToken.IsCancellationRequested)
            {
                var consumerHelper = new ConsumerWrapper(_consumerConfig, "topicfeed2");
                string orderRequest = consumerHelper.ReadMessage();

                //TODO:: Process Order
                Console.WriteLine($"Info: OrderHandler => Processing the order for {orderRequest}");
            }
        }
    }
}
