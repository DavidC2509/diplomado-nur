using Core.Cqrs.Domain;
using Core.Cqrs.Domain.Domain;

namespace Template.Domain.MedicalConsultationAggregate
{
    public class Consultation : BaseEntity, IAggregateRoot
    {
        public string Descripcion { get; private set; }
        public bool Status { get; private set; }
        public Guid IdConsultExternal { get; private set; }
        public Guid IdClient { get; private set; }

        private readonly List<HistoryConsultation> _historyConsultations;
        public IEnumerable<HistoryConsultation> HistoryConsultations => _historyConsultations.AsReadOnly();

        private Consultation()
        {
            Descripcion = string.Empty;
            _historyConsultations = [];
        }

        internal Consultation(string name, Guid idConsultExternal, Guid idClient, bool status) : this()
        {
            Descripcion = name;
            IdConsultExternal = idConsultExternal;
            Status = status;
            IdClient = idClient;
        }

        public static Consultation CreateConsult(string name, Guid idConsultExternal, Guid idClient, bool status)
            => new(name, idConsultExternal, idClient, status);


    }
}