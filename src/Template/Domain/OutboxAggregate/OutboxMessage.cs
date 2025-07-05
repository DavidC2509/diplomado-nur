using Core.Cqrs.Domain;
using Core.Cqrs.Domain.Domain;
using System.Text.Json;

namespace Template.Domain.OutboxAggregate
{
    public class OutboxMessage : BaseEntity, IAggregateRoot
    {
        public string EventType { get; set; }
        public string EventVersion { get; set; }
        public DateTime Timestamp { get; set; }
        public JsonElement Body { get; set; }
        public bool Sent { get; set; } = false;
        public string Source { get; set; }
        public DateTime? TimestampSend { get; set; }
        public string TraceId { get; set; }

        private OutboxMessage()
        {
            EventType = string.Empty;
            Source = string.Empty;
            Timestamp = DateTime.Now.ToUniversalTime();
            EventVersion = string.Empty;
            Sent = false;
            TraceId = string.Empty;
        }

        internal OutboxMessage(string eventType, JsonElement body, string traceId)
        {
            EventType = eventType;
            EventVersion = "1.0.0";
            Body = body;
            Source = "medical_consultation";
            Timestamp = DateTime.Now.ToUniversalTime();
            Sent = false;
            TraceId = traceId;
        }

        public static OutboxMessage StoreOutbox(string eventType, JsonElement body, string traceId)
            => new(eventType, body, traceId);

        public void UpdateSendOutbox()
        {
            Sent = true;
            TimestampSend = DateTime.Now.ToUniversalTime();
        }
    }
}