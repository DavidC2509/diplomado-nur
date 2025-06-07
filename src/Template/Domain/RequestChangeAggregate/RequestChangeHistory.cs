using Core.Cqrs.Domain;
using Core.Cqrs.Domain.Domain;
using MediatR;
using Template.Domain.RequestChangeAggregate.Events;

namespace Template.Domain.RequestChangeAggregate
{
    public class RequestChangeHistory : BaseEntity, IAggregateRoot, IDataTenantId
    {
        public Guid AppointmentGuid { get; private set; }
        public Guid IdClient { get; private set; }
        public DateTime PreviusDate { get; private set; }
        public DateTime NewDate { get; private set; }
        public DateTime RegisterDate { get; private set; }

        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents.AsReadOnly();
        private readonly List<INotification> _domainEvents = [];

        public IReadOnlyCollection<INotification> DomainEventsAwait => _domainEventsAwait.AsReadOnly();
        private readonly List<INotification> _domainEventsAwait = [];


        private RequestChangeHistory()
        {

        }

        internal RequestChangeHistory(Guid appointmentGuid, Guid idClient, DateTime previusDate, DateTime newDate) : this()
        {
            AppointmentGuid = appointmentGuid;
            IdClient = idClient;
            PreviusDate = previusDate;
            NewDate = newDate;
            var localDateTime = DateTime.Now; // Hora local
            RegisterDate = localDateTime.ToUniversalTime();

            AddNotifiedNutrionEvent();
        }

        public static RequestChangeHistory CreateChangeHistory(Guid appointmentGuid, Guid idClient, DateTime previusDate, DateTime newDate)
            => new(appointmentGuid, idClient, previusDate, newDate);

        public void AddNotifiedNutrionEvent()
        {
            var categoryDefaultEvent = new UpdateDateDeliveryNotifiedNutrionEvent(IdClient, PreviusDate, NewDate);
            _domainEventsAwait.Add(categoryDefaultEvent);
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