using MediatR;
using Template.Domain.ClientAggregate.Events;
using Template.Domain.Interfaz;
using Template.Services.Models;

namespace Template.Services.HandlerEvents
{
    public class AddresStoreEventHandler : INotificationHandler<AddresStoreEvent>
    {
        private readonly IOutboxService _outbox;

        public AddresStoreEventHandler(IOutboxService outbox)
        {
            _outbox = outbox;
        }

        public async Task Handle(AddresStoreEvent domainEvent, CancellationToken cancellationToken)
        {
            var integrationEvent = new AddressStoredIntegrationEvent
            {
                ClientGuid = domainEvent.ClientGuid,
                Street = domainEvent.Street,
                City = domainEvent.City,
                Latituded = domainEvent.Latituded,
                Longitud = domainEvent.Longitud,
            };

            await _outbox.SaveAsync(integrationEvent, "user-addres-update", cancellationToken);
        }
    }
}
