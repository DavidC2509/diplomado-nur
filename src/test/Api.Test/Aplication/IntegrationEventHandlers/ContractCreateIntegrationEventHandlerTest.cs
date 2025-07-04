using Azure.Messaging.EventHubs.Processor;
using System.Text.Json;
using Template.Services.EventsRecive;
using Template.Services.EventsRecive.Handler;
using Template.Services.EventsRecive.Models;

namespace Api.Test.Aplication.IntegrationEventHandlers
{
    internal class ContractCreateIntegrationEventHandlerTest
    {
        private readonly ContractCreateIntegrationEventHandler _handler;

        public ContractCreateIntegrationEventHandlerTest()
        {
            _handler = new ContractCreateIntegrationEventHandler();
        }

        [Test]
        public async Task MessageHandler_ShouldProcessValidMessage()
        {
            // Arrange
            var contractData = new ContracRecieveHandler
            {
                Id = Guid.NewGuid(),
                ClientId = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                NutritionalPlanId = Guid.NewGuid(),
                PlanDetails = "Weekly meal plan with nutritional guidelines."
            };


            var integrationEvent = new ModelReciveData<ContracRecieveHandler>
            {
                Source = "ContractService",
                EventType = "ContractCreated",
                Body = contractData
            };

            var message = JsonSerializer.Serialize(integrationEvent);

            // Act
            await _handler.MessageHandler(message);

            // Assert
            // El método no lanza excepciones y procesa el mensaje correctamente
            Assert.Pass("Message processed successfully");
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