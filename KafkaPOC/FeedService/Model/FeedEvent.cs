namespace FeedService.Model
{
    public class FeedEvent
    {
        public string Id { get; set; }
        public string AggregatedId { get; set; }
        public string Category { get; set; }
        public string Status { get; set; }
        public string Name { get; set; }
        public string Tag { get; set; }
        public int Value { get; set; }
        public string Timestamp { get; set; }
    }
}
