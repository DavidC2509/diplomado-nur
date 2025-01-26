using Core.Cqrs.CommandAndQueryHandler;
using Core.Cqrs.Domain.Repository;
using Template.Domain.ClientAggregate;

namespace Template.Services.Command.ClientCommand
{
    public class StoreClientCommandHandler : BaseCommandHandler<IRepository<Client>, StoreClientCommand, bool>
    {

        public StoreClientCommandHandler(IRepository<Client> repository) : base(repository)
        {
        }

        public async override Task<bool> Handle(StoreClientCommand request, CancellationToken cancellationToken)
        {

            var client = Client.CreateClient(request.Name, request.Phone, request.Email);
            _repository.Add(client);
            await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return true;
        }
    }
}
