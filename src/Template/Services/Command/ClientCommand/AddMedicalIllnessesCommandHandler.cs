using Core.Cqrs.CommandAndQueryHandler;
using Core.Cqrs.Domain.Repository;
using Template.Domain.ClientAggregate;

namespace Template.Services.Command.ClientCommand
{
    public class AddMedicalIllnessesCommandHandler : BaseCommandHandler<IRepository<Client>, AddMedicalIllnessesCommand, bool>
    {

        public AddMedicalIllnessesCommandHandler(IRepository<Client> repository) : base(repository)
        {
        }

        public async override Task<bool> Handle(AddMedicalIllnessesCommand request, CancellationToken cancellationToken)
        {

            var client = await _repository.GetByIdAsync(request.IdClient, cancellationToken);
            client.AddMedicalIllnesses(request.Name, request.Descripcion, request.Type);
            _repository.Update(client);
            await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return true;
        }
    }
}