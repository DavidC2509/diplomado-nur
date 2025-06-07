using MediatR;

namespace Template.Domain.ClientAggregate.Events
{
    public class AddresStoreEvent : INotification
    {
        public Guid ClientGuid { get; }
        public DateTime OccurredOn { get; }
        public string Street { get; }
        public string City { get; }
        public decimal Latituded { get; }
        public decimal Longitud { get; }
        public Guid AddresId { get; }


        public AddresStoreEvent(Guid clientGuid, string street, string city, decimal latituded, decimal longitud, Guid addresId)
        {
            ClientGuid = clientGuid;
            OccurredOn = DateTime.Now;
            Street = street;
            City = city;
            Latituded = latituded;
            Longitud = longitud;
            AddresId = addresId;
        }
    }
}