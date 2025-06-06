using Core.Cqrs.Domain;
using Core.Cqrs.Domain.Domain;
using System.Text.Json;

namespace Template.Domain.OutboxAggregate
{
    public class OutboxMessage : BaseEntity, IAggregateRoot
    {
        public string EventType { get; set; }
        public string EventVersion { get; set; }
        public DateTime CreatedAt { get; set; }
        public JsonElement Body { get; set; }
        public bool Sent { get; set; } = false;
        public string Source { get; set; }
        public DateTime? TimestampSend { get; set; }

        private OutboxMessage()
        {
            EventType = string.Empty;
            Source = string.Empty;
            CreatedAt = DateTime.Now.ToUniversalTime();
            EventVersion = string.Empty;
            Sent = false;
        }

        internal OutboxMessage(string eventType, JsonElement body)
        {
            EventType = eventType;
            EventVersion = "1.0.0";
            Body = body;
            Source = "medical_consultation";
        }

        public static OutboxMessage StoreOutbox(string eventType, JsonElement body)
            => new(eventType, body);

        public void UpdateSendOutbox()
        {
            Sent = true;
            TimestampSend = DateTime.Now.ToUniversalTime();
        }
    }
}
