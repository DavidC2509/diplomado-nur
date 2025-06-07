using MediatR;
using Template.Domain.Interfaz;
using Template.Domain.RequestChangeAggregate.Events;
using Template.Services.Models;

namespace Template.Services.HandlerEvents
{
    public class UpdateDateDeliveryNotifiedNutrionHandlerEvent : INotificationHandler<UpdateDateDeliveryNotifiedNutrionEvent>
    {

        private readonly IOutboxService _outbox;

        public UpdateDateDeliveryNotifiedNutrionHandlerEvent(IOutboxService outbox)
        {
            _outbox = outbox;
        }

        public async Task Handle(UpdateDateDeliveryNotifiedNutrionEvent notification, CancellationToken cancellationToken)
        {
            var integrationEvent = new UpdateDeliveryDateEvent
            {
                AddresGuid = notification.AddresGuid,
                NewDate = notification.NewDate,
                IdClient = notification.ClientId,
                PreviusDate = notification.PreviusDate
            };

            await _outbox.SaveAsync(integrationEvent, "delevery-date-update", cancellationToken);

        }
    }
}