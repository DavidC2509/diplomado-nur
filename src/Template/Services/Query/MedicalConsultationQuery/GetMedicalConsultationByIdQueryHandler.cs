using AutoMapper;
using Core.Cqrs.CommandAndQueryHandler;
using Core.Cqrs.Domain.Repository;
using Template.Domain.MedicalConsultationAggregate;
using Template.Domain.MedicalConsultationAggregate.Specification;
using Template.Services.Models;

namespace Template.Services.Query.MedicalConsultationQuery
{
    public class GetMedicalConsultationByIdQueryHandler : BaseQueryHandler<Consultation, GetMedicalConsultationByIdQuery, MedicalConsultModel>
    {
        private readonly IMapper _mapper;

        public GetMedicalConsultationByIdQueryHandler(IReadRepository<Consultation> repository, IMapper mapper) : base(repository)
        {
            _mapper = mapper;
        }

        public async override Task<MedicalConsultModel> Handle(GetMedicalConsultationByIdQuery request, CancellationToken cancellationToken)
        {
            var spec = new ConsultationByIdSpec(request.Id);
            var account = await _repository.FirstOrDefaultAsync(spec, cancellationToken);

            var resultMapper = _mapper.Map<MedicalConsultModel>(account);
            return resultMapper;
        }
    }
}