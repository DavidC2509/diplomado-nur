using Core.Cqrs.CommandAndQueryHandler;
using Core.Cqrs.Domain.Repository;
using Template.Domain.ClientAggregate;
using Template.Domain.ClientAggregate.Specification;

namespace Template.Services.Command.ClientCommand
{
    public class ModifiedRequestChangeDeliveryBlockCommandHandler : BaseCommandHandler<IRepository<Client>, ModifiedRequestChangeDeliveryBlockCommand, bool>
    {

        public ModifiedRequestChangeDeliveryBlockCommandHandler(IRepository<Client> repository) : base(repository)
        {
        }

        public async override Task<bool> Handle(ModifiedRequestChangeDeliveryBlockCommand request, CancellationToken cancellationToken)
        {
            var specClient = new GetClientByIdSpec(request.IdClient);
            var client = await _repository.FirstOrDefaultAsync(specClient, cancellationToken);
            client?.UpdateDateAddres(request.AddresGuid, request.ToDate, request.FromDate, request.IdClient);
            _repository.Update(client);
            await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return true;
        }
    }
}