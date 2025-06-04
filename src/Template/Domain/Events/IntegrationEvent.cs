namespace Template.Domain.Events
{
    public class IntegrationEvent
    {
        public string TopicName { get; set; } = string.Empty;
        public string SubscriptionName { get; set; } = string.Empty;
    }
}
