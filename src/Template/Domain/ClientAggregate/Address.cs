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
        public DateTime DateDeliveryDate { get; private set; }
        public DateTime? DateToDeliveryIgnore { get; private set; }
        public DateTime? DateFromDeliveryIgnore { get; private set; }

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

        internal Address(string street, string city, decimal latituded, decimal longitud, DateTime dateDeliveryDate) : this()
        {
            Street = street;
            City = city;
            Latituded = latituded;
            Longitud = longitud;
            DateDeliveryDate = dateDeliveryDate.ToUniversalTime();
            Status = true;
        }

        public static Address StoreAddres(string street, string city, decimal latituded, decimal longitud, DateTime dateDeliveryDate)
        => new(street, city, latituded, longitud, dateDeliveryDate);

        public void UpdateStatus(bool status)
        {
            Status = status;
        }

        public void UpdateDateBlockDelivery(DateTime toDate, DateTime fromDate, Guid clientGuid)
        {
            var eventAddres = new UpdateDateBlockDeliveryEvent(clientGuid, fromDate, toDate, Id);
            DateDeliveryDate = toDate;
            DateToDeliveryIgnore = toDate;
            DateFromDeliveryIgnore = fromDate;
            _domainEvents.Add(eventAddres);
        }


        public void NotificationEvent(Guid clientGuid)
        {
            var eventAddres = new AddresStoreEvent(clientGuid, Street, City, Latituded, Longitud, Id);
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