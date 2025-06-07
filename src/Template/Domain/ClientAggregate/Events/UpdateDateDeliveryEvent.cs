using MediatR;

namespace Template.Domain.ClientAggregate.Events
{
    public class UpdateDateDeliveryEvent : INotification
    {
        public Guid ClientGuid { get; set; }
        public DateTime PreviusDate { get; set; }
        public DateTime NewDate { get; set; }
        public Guid AddressGuid { get; set; }

        public UpdateDateDeliveryEvent(Guid clientGuid, DateTime previusDate, DateTime newDate, Guid addresGuid)
        {
            ClientGuid = clientGuid;
            PreviusDate = previusDate;
            NewDate = newDate;
            AddressGuid = addresGuid;
        }
    }
}
