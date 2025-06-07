using MediatR;

namespace Template.Domain.RequestChangeAggregate.Events
{
    public class UpdateDateDeliveryNotifiedNutrionEvent : INotification
    {
        public Guid ClientId { get; set; }
        public DateTime PreviusDate { get; set; }
        public DateTime NewDate { get; set; }
        public Guid AddresGuid { get; set; }

        public UpdateDateDeliveryNotifiedNutrionEvent(Guid clientId, DateTime previusDate, DateTime newDate)
        {
            ClientId = clientId;
            PreviusDate = previusDate;
            NewDate = newDate;
            AddresGuid = Guid.NewGuid();
        }
    }
}