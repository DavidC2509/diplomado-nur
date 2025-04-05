using MediatR;
using Template.Services.Models;

namespace Template.Services.Query.MedicalConsultationQuery
{
    public class ListMedicalConsultationByClientQuery : IRequest<IEnumerable<MedicalConsultModel>>
    {
        public Guid IdClient { get; set; }

        public ListMedicalConsultationByClientQuery(Guid idClient)
        {
            IdClient = idClient;
        }
    }
}