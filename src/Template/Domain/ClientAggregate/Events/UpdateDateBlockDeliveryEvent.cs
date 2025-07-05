using MediatR;

namespace Template.Domain.ClientAggregate.Events
{
    public class UpdateDateBlockDeliveryEvent : INotification
    {
        public Guid ClientGuid { get; set; }
        public DateTime PreviusDate { get; set; }
        public DateTime NewDate { get; set; }
        public Guid AddressGuid { get; set; }

        public UpdateDateBlockDeliveryEvent(Guid clientGuid, DateTime previusDate, DateTime newDate, Guid addresGuid)
        {
            ClientGuid = clientGuid;
            PreviusDate = previusDate;
            NewDate = newDate;
            AddressGuid = addresGuid;
        }
    }
}