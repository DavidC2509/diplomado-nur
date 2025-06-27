//using Moq;
//using Template.Domain.RequestChangeAggregate.Events;
//using Template.Domain.Interfaz;
//using Template.Services.HandlerEvents;
//using Template.Services.Models.EventsModels;

//namespace Api.Test.Aplication.EventHandlers
//{
//    internal class UpdateDateDeliveryNotifiedNutrionHandlerEventTest
//    {
//        private readonly Mock<IOutboxService> _outboxService;
//        private readonly UpdateDateDeliveryNotifiedNutrionHandlerEvent _handler;

//        public UpdateDateDeliveryNotifiedNutrionHandlerEventTest()
//        {
//            _outboxService = new Mock<IOutboxService>();
//            _handler = new UpdateDateDeliveryNotifiedNutrionHandlerEvent(_outboxService.Object);
//        }

//        [Test]
//        public async Task Handle_ShouldSaveIntegrationEvent()
//        {
//            // Arrange
//            var clientId = Guid.NewGuid();
//            var newDate = DateTime.Now.AddDays(1);
//            var domainEvent = new UpdateDateDeliveryNotifiedNutrionEvent(clientId, newDate);

//            _outboxService.Setup(x => x.SaveAsync(It.IsAny<UpdateDeliveryDateEvent>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
//                         .Returns(Task.CompletedTask);

//            // Act
//            await _handler.Handle(domainEvent, CancellationToken.None);

//            // Assert
//            _outboxService.Verify(x => x.SaveAsync(
//                It.Is<UpdateDeliveryDateEvent>(e => 
//                    e.ClientGuid == clientId &&
//                    e.NewDate == newDate),
//                "UPDATE_DELIVERY_DATE",
//                It.IsAny<CancellationToken>()), Times.Once);
//        }
//    }
//} 