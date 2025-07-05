using MediatR;
using Template.Domain.ClientAggregate.Events;
using Template.Domain.Interfaz;
using Template.Services.Models.EventsModels;

namespace Template.Services.HandlerEvents
{
    public class UpdateDateBlockDeliveryHandlerEvent : INotificationHandler<UpdateDateBlockDeliveryEvent>
    {

        private readonly IOutboxService _outbox;

        public UpdateDateBlockDeliveryHandlerEvent(IOutboxService outbox)
        {
            _outbox = outbox;
        }

        public async Task Handle(UpdateDateBlockDeliveryEvent notification, CancellationToken cancellationToken)
        {
            var integrationEvent = new UpdateDeliveryBlockDateEvent
            {
                AddressGuid = notification.AddressGuid,
                ToDate = notification.NewDate,
                ClientGuid = notification.ClientGuid,
                FromDate = notification.PreviusDate
            };

            await _outbox.SaveAsync(integrationEvent, "DELIVERY_DATE_BLOCK_UPDATE", cancellationToken);

        }
    }
}