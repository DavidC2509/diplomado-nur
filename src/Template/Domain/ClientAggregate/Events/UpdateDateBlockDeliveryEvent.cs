using MediatR;

namespace Template.Domain.ClientAggregate.Events
{
    public class UpdateDateBlockDeliveryEvent : INotification
    {
        public Guid ClientGuid { get; set; }
        public DateTime PreviusDate { get; set; }
        public DateTime NewDate { get; set; }
        public Guid AddressGuid { get; set; }

        public UpdateDateBlockDeliveryEvent(Guid clientGuid, DateTime fromDate, DateTime toDate, Guid addresGuid)
        {
            ClientGuid = clientGuid;
            PreviusDate = fromDate;
            NewDate = toDate;
            AddressGuid = addresGuid;
        }
    }
}