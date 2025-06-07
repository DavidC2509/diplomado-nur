using Core.Cqrs.CommandAndQueryHandler;
using Core.Cqrs.Domain.Repository;
using Template.Domain.ClientAggregate;

namespace Template.Services.Command.ClientCommand
{
    public class ModifiedRequestChangeDeliveryCommandHandler : BaseCommandHandler<IRepository<Client>, ModifiedRequestChangeDeliveryCommand, bool>
    {

        public ModifiedRequestChangeDeliveryCommandHandler(IRepository<Client> repository) : base(repository)
        {
        }

        public async override Task<bool> Handle(ModifiedRequestChangeDeliveryCommand request, CancellationToken cancellationToken)
        {
            var client = await _repository.GetByIdAsync(request.IdClient, cancellationToken);
            client?.UpdateDateAddres(request.AddresGuid, request.DateDelivery, request.IdClient);
            _repository.Update(client);
            await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return true;
        }
    }
}