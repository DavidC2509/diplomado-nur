﻿using Core.Cqrs.CommandAndQueryHandler;
using Core.Cqrs.Domain.Repository;
using Template.Domain.ClientAggregate;

namespace Template.Services.Command.ClientCommand
{
    public class AddAddresByClientCommandHandler : BaseCommandHandler<IRepository<Client>, AddAddresByClientCommand, bool>
    {

        public AddAddresByClientCommandHandler(IRepository<Client> repository) : base(repository)
        {
        }

        public async override Task<bool> Handle(AddAddresByClientCommand request, CancellationToken cancellationToken)
        {
            var client = await _repository.GetByIdAsync(request.IdClient);
            client.AddAddres(request.Street, request.City, request.Longitud, request.Latituded);
            _repository.Update(client);
            await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return true;
        }
    }
}
