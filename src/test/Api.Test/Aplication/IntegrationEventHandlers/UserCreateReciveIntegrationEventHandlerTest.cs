using Azure.Messaging.EventHubs.Processor;
using Core.Cqrs.Domain.Repository;
using Moq;
using System.Text.Json;
using Template.Domain.ClientAggregate;
using Template.Services.EventsRecive;
using Template.Services.EventsRecive.Handler;
using Template.Services.EventsRecive.Models;

namespace Api.Test.Aplication.IntegrationEventHandlers
{
    internal class UserCreateReciveIntegrationEventHandlerTest
    {
        private readonly Mock<IRepository<Client>> _userRepository;
        private readonly UserCreateReciveIntegrationEventHandler _handler;

        public UserCreateReciveIntegrationEventHandlerTest()
        {
            _userRepository = new Mock<IRepository<Client>>();
            _handler = new UserCreateReciveIntegrationEventHandler(_userRepository.Object);
        }

        [Test]
        public async Task MessageHandler_ShouldProcessValidMessage()
        {
            // Arrange
            var userData = new UserCreateReciveHandler
            {
                Id = Guid.NewGuid(),
                Username = "usuario_test",
                Email = "usuario@test.com",
                FullName = "Usuario Test",
                CreatedAt = DateTime.Now
            };

            var integrationEvent = new ModelReciveData<UserCreateReciveHandler>
            {
                Source = "UserService",
                EventType = "UserCreated",
                Body = userData
            };

            var message = JsonSerializer.Serialize(integrationEvent);

            _userRepository.Setup(x => x.Add(It.IsAny<Client>()))
                         .Returns(It.IsAny<Client>());
            _userRepository.Setup(x => x.UnitOfWork.SaveEntitiesAsync(It.IsAny<CancellationToken>()))
                         .ReturnsAsync(true);

            // Act
            await _handler.MessageHandler(message);

            // Assert
            _userRepository.Verify(x => x.Add(It.IsAny<Client>()), Times.Once);
            _userRepository.Verify(x => x.UnitOfWork.SaveEntitiesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }


        [Test]
        public async Task ErrorHandler_ShouldHandleError()
        {
            // Arrange
            var errorEventArgs = new ProcessErrorEventArgs(
                "test-partition",
                "test-operation",
                new Exception("Test error"),
                CancellationToken.None
            );

            // Act & Assert
            Assert.DoesNotThrowAsync(async () => await _handler.ErrorHandler(errorEventArgs));
        }
    }
}