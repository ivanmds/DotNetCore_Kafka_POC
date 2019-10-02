using System.Threading.Tasks;
using FeedService.Kafka.Producers;
using FeedService.Model;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace FeedService.Services
{
    public class FeedEventService : Feed.FeedBase
    {
        private readonly ILogger<FeedEventService> _logger;
        private readonly IFeedEventProducer _feedEvent;

        public FeedEventService(ILogger<FeedEventService> logger, IFeedEventProducer feedEvent)
        {
            _logger = logger;
            _feedEvent = feedEvent;
        }

        public override Task<FeedReplay> ToFeed(FeedRequest request, ServerCallContext context)
        {
            _feedEvent.PublishAsync(new FeedEvent { }).Wait();
            return base.ToFeed(request, context);
        }
    }
}
