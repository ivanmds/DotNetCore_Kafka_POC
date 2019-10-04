using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Feed.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedController : ControllerBase
    {

        [HttpGet("{message}")]
        public async Task Add(string message)
        {
            string kafkaEndpoint = "broker:29092";

            var config = new ProducerConfig { BootstrapServers = kafkaEndpoint };

            using var p = new ProducerBuilder<Null, string>(config).Build();
            await p.ProduceAsync("topicfeed2", new Message<Null, string> { Value = message });
        }
    }
}