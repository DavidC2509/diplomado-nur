using Core.Cqrs.CommandAndQueryHandler;
using Core.Cqrs.Domain.Repository;
using Template.Domain.ClientAggregate;
using Template.Domain.ClientAggregate.Specification;

namespace Template.Services.Command.ClientCommand
{
    public class ModifiedRequestChangeDeliveryCommandHandler : BaseCommandHandler<IRepository<Client>, ModifiedRequestChangeDeliveryCommand, bool>
    {

        public ModifiedRequestChangeDeliveryCommandHandler(IRepository<Client> repository) : base(repository)
        {
        }

        public async override Task<bool> Handle(ModifiedRequestChangeDeliveryCommand request, CancellationToken cancellationToken)
        {
            var specClient = new GetClientByIdSpec(request.IdClient);
            var client = await _repository.FirstOrDefaultAsync(specClient, cancellationToken);
            client?.UpdateDateAddres(request.AddresGuid, request.DateDelivery, request.IdClient);
            _repository.Update(client);
            await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return true;
        }
    }
}