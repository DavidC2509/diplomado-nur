using MediatR;
using Template.Domain.ClientAggregate.Events;
using Template.Domain.Interfaz;
using Template.Services.Models.EventsModels;

namespace Template.Services.HandlerEvents
{
    public class UpdateDateDeliveryHandlerEvent : INotificationHandler<UpdateDateDeliveryEvent>
    {

        private readonly IOutboxService _outbox;

        public UpdateDateDeliveryHandlerEvent(IOutboxService outbox)
        {
            _outbox = outbox;
        }

        public async Task Handle(UpdateDateDeliveryEvent notification, CancellationToken cancellationToken)
        {
            var integrationEvent = new UpdateDeliveryDateEvent
            {
                AddressGuid = notification.AddressGuid,
                NewDate = notification.NewDate,
                ClientGuid = notification.ClientGuid,
                PreviousDate = notification.PreviusDate
            };

            await _outbox.SaveAsync(integrationEvent, "DELIVERY_DATE_UPDATE", cancellationToken);

        }
    }
}