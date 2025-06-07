using MediatR;
using Template.Domain.ClientAggregate.Events;
using Template.Domain.Interfaz;
using Template.Services.Models.EventsModels;

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
                Latitude = domainEvent.Latituded,
                Longitude = domainEvent.Longitud,
                AddressId = domainEvent.AddresId
            };

            await _outbox.SaveAsync(integrationEvent, "USER_ADDRESS_UPDATE", cancellationToken);
        }
    }
}