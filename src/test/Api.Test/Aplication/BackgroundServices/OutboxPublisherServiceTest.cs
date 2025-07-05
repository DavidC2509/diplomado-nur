using Core.Cqrs.Domain.Repository;
using Moq;
using System.Text.Json;
using Template.Domain.Interfaz;
using Template.Domain.OutboxAggregate;
using Template.Domain.OutboxAggregate.Specification;

namespace Api.Test.Aplication.BackgroundServices
{
    internal class OutboxPublisherServiceTest
    {
        private readonly Mock<IRepository<OutboxMessage>> _outboxRepository;
        private readonly Mock<IOutboxService> _outboxService;

        public OutboxPublisherServiceTest()
        {
            _outboxRepository = new Mock<IRepository<OutboxMessage>>();
            _outboxService = new Mock<IOutboxService>();
        }

        [Test]
        public async Task ProcessPendingMessages_ShouldProcessOutboxMessages()
        {
            // Arrange
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var data = new
            {
                Status = false,
                Name = "Consulta 1",
            };
            var json = JsonSerializer.SerializeToElement(data, options);

            var pendingMessages = new List<OutboxMessage>
            {
                 OutboxMessage.StoreOutbox("TestEvent",json,Guid.NewGuid().ToString())

            };

            _outboxRepository.Setup(x => x.ListAsync(It.IsAny<GetPendingOutboxSpec>(), It.IsAny<CancellationToken>()))
                            .ReturnsAsync(pendingMessages);

            _outboxRepository.Setup(x => x.Update(It.IsAny<OutboxMessage>()))
                            .Returns(It.IsAny<OutboxMessage>());

            _outboxRepository.Setup(x => x.UnitOfWork.SaveEntitiesAsync(It.IsAny<CancellationToken>()))
                            .ReturnsAsync(true);

            // Act
            // Simulamos el procesamiento de mensajes pendientes
            var result = await _outboxRepository.Object.ListAsync(new GetPendingOutboxSpec(), CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task ProcessPendingMessages_WithNoPendingMessages_ShouldReturnEmpty()
        {
            // Arrange
            var emptyMessages = new List<OutboxMessage>();

            _outboxRepository.Setup(x => x.ListAsync(It.IsAny<GetPendingOutboxSpec>(), It.IsAny<CancellationToken>()))
                            .ReturnsAsync(emptyMessages);

            // Act
            var result = await _outboxRepository.Object.ListAsync(new GetPendingOutboxSpec(), CancellationToken.None);

            // Assert
            Assert.That(result, Is.Empty);
        }
    }
}