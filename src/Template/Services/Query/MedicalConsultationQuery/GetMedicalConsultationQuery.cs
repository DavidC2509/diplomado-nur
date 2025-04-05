using MediatR;
using Template.Services.Models;

namespace Template.Services.Query.MedicalConsultationQuery
{
    public class GetMedicalConsultationByIdQuery : IRequest<MedicalConsultModel>
    {
        public Guid Id { get; set; }

        public GetMedicalConsultationByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}