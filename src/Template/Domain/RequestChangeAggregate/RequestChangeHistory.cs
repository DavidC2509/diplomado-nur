using Core.Cqrs.Domain;
using Core.Cqrs.Domain.Domain;
using MediatR;
using Template.Domain.RequestChangeAggregate.Events;

namespace Template.Domain.RequestChangeAggregate
{
    public class RequestChangeHistory : BaseEntity, IAggregateRoot, IDataTenantId
    {
        public Guid IdAppointment { get; private set; }
        public Guid IdClient { get; private set; }

        public DateTime PreviusDate { get; private set; }
        public DateTime NewDate { get; private set; }
        public DateTime RegisterDate { get; private set; }


        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents.AsReadOnly();
        private List<INotification> _domainEvents = new();

        public IReadOnlyCollection<INotification> DomainEventsAwait => _domainEventsAwait.AsReadOnly();
        private List<INotification> _domainEventsAwait = new();



        private RequestChangeHistory()
        {

        }

        internal RequestChangeHistory(Guid idAppointment, Guid idClient, DateTime previusDate, DateTime newDate) : this()
        {
            IdAppointment = idAppointment;
            IdClient = idClient;
            PreviusDate = previusDate;
            NewDate = newDate;
            var localDateTime = DateTime.Now; // Hora local
            RegisterDate = localDateTime.ToUniversalTime();

            AddNotifiedNutrionEvent();
        }

        public static RequestChangeHistory CreateChangeHistory(Guid idAppointment, Guid idClient, DateTime previusDate, DateTime newDate)
            => new(idAppointment, idClient, previusDate, newDate);

        public void AddNotifiedNutrionEvent()
        {
            var categoryDefaultEvent = new NotifiedNutrionEvent(IdClient);
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