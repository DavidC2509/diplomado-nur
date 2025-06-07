using Azure.Messaging.EventHubs.Processor;
using System.Text.Json;
using Template.Domain.Interfaz.EventBus;

namespace Template.Services.EventsRecive.Handler
{
    public class ContractCreateIntegrationEventHandler : IIntegrationEventHandler
    {
        public async Task MessageHandler(string message)
        {
            var integrationEvent = JsonSerializer.Deserialize<ModelReciveData<UserCreateReciveHandler>>(message);

            Console.WriteLine($"📥 Mensaje recibido desde EventHub: {integrationEvent?.Source}, {integrationEvent?.EventType}");

            await Task.CompletedTask;
        }

        public Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine($"❌ Error en EventHub '{args.PartitionId}': {args.Exception.Message}");
            return Task.CompletedTask;
        }
    }
}