using Core.Cqrs.CommandAndQueryHandler;
using Core.Cqrs.Domain.Repository;
using Template.Domain.MedicalConsultationAggregate;

namespace Template.Services.Command.MedicalConsultCommand
{
    public class StoreMedicalConsultCommandHandler : BaseCommandHandler<IRepository<Consultation>, StoreMedicalConsultCommand, bool>
    {

        public StoreMedicalConsultCommandHandler(IRepository<Consultation> repository) : base(repository)
        {
        }

        public async override Task<bool> Handle(StoreMedicalConsultCommand request, CancellationToken cancellationToken)
        {
            var consultation = Consultation.CreateConsult(request.Name, request.IdConsultExternal, request.IdClient, request.Status);
            _repository.Add(consultation);
            await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return true;
        }
    }
}