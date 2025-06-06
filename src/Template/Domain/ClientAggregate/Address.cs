using Core.Cqrs.Domain;
using Core.Cqrs.Domain.Domain;
using MediatR;
using Template.Domain.ClientAggregate.Events;

namespace Template.Domain.ClientAggregate
{
    public class Address : BaseEntity, IAggregateChild<Client>, IAggregateRoot, IDataTenantId
    {
        public string Street { get; private set; }
        public string City { get; private set; }
        public decimal Latituded { get; private set; }
        public decimal Longitud { get; private set; }
        public bool Status { get; private set; }
        private readonly List<AddressHistory> _history;
        public IEnumerable<AddressHistory> History => _history.AsReadOnly();

        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents.AsReadOnly();
        private readonly List<INotification> _domainEvents = [];

        public IReadOnlyCollection<INotification> DomainEventsAwait => _domainEventsAwait.AsReadOnly();
        private readonly List<INotification> _domainEventsAwait = [];


        private Address()
        {
            Street = string.Empty;
            City = string.Empty;
            Latituded = 0;
            Longitud = 0;
            Status = false;
            _history = [];
        }

        internal Address(string street, string city, decimal latituded, decimal longitud, Guid clientGuid) : this()
        {
            Street = street;
            City = city;
            Latituded = latituded;
            Longitud = longitud;
            Status = true;
            NotificationEvent(clientGuid);

        }

        public static Address StoreAddres(string street, string city, decimal latituded, decimal longitud, Guid clientGuid)
        => new(street, city, latituded, longitud, clientGuid);

        public void UpdateStatus(bool status)
        {
            Status = status;
        }

        public void NotificationEvent(Guid clientGuid)
        {
            var eventAddres = new AddresStoreEvent(clientGuid, Street, City, Latituded, Longitud);
            _domainEvents.Add(eventAddres);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }

        public void ClearDomainEventsAwait()
        {
            _domainEventsAwait.Clear();
        }
    }
}