using MediatR;

namespace Template.Domain.RequestChangeAggregate.Events
{
    public class NotifiedNutrionEvent : INotification
    {
        public Guid ClientId { get; set; }

        public NotifiedNutrionEvent(Guid clientId)
        {
            ClientId = clientId;
        }
    }
}