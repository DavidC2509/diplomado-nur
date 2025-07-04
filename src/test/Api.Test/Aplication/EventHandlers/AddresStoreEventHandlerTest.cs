using Moq;
using Template.Domain.ClientAggregate.Events;
using Template.Domain.Interfaz;
using Template.Services.HandlerEvents;
using Template.Services.Models.EventsModels;

namespace Api.Test.Aplication.EventHandlers
{
    internal class AddresStoreEventHandlerTest
    {
        private readonly Mock<IOutboxService> _outboxService;
        private readonly AddresStoreEventHandler _handler;

        public AddresStoreEventHandlerTest()
        {
            _outboxService = new Mock<IOutboxService>();
            _handler = new AddresStoreEventHandler(_outboxService.Object);
        }

        [Test]
        public async Task Handle_ShouldSaveIntegrationEvent()
        {
            // Arrange
            var clientId = Guid.NewGuid();
            var addressId = Guid.NewGuid();
            var domainEvent = new AddresStoreEvent(
                clientId,
                "Av. 6 de Agosto",
                "La Paz",
                -16.4897m,
                -68.1193m,
                addressId
            );

            _outboxService.Setup(x => x.SaveAsync(It.IsAny<AddressStoredIntegrationEvent>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                         .Returns(Task.CompletedTask);

            // Act
            await _handler.Handle(domainEvent, CancellationToken.None);

            // Assert
            _outboxService.Verify(x => x.SaveAsync(
                It.Is<AddressStoredIntegrationEvent>(e =>
                    e.ClientGuid == clientId &&
                    e.AddressId == addressId &&
                    e.Street == "Av. 6 de Agosto" &&
                    e.City == "La Paz" &&
                    e.Latitude == -16.4897m &&
                    e.Longitude == -68.1193m),
                "USER_ADDRESS_UPDATE",
                It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}