using Azure.Messaging.EventHubs.Processor;
using Core.Cqrs.Domain.Repository;
using System.Text.Json;
using Template.Domain.ClientAggregate;
using Template.Domain.Interfaz.EventBus;

namespace Template.Services.EventsRecive.Handler
{
    public class UserCreateReciveIntegrationEventHandler : IIntegrationEventHandler
    {
        private readonly IRepository<Client> _userRepository;

        public UserCreateReciveIntegrationEventHandler(IRepository<Client> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task MessageHandler(string message)
        {
            var integrationEvent = JsonSerializer.Deserialize<ModelReciveData<UserCreateReciveHandler>>(message, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true // <- Esta línea permite mapear camelCase a PascalCase
            });
            Console.WriteLine($"📥 Mensaje recibido desde EventHub: {integrationEvent?.Source}, {integrationEvent?.EventType}");

            if (integrationEvent != null)
            {
                var client = Client.CreateClient(integrationEvent.Body.FullName, "784512369", integrationEvent.Body.Email);
                _userRepository.Add(client);
                await _userRepository.UnitOfWork.SaveEntitiesAsync(CancellationToken.None);
            }

            await Task.CompletedTask;
        }

        public Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine($"❌ Error en EventHub '{args.PartitionId}': {args.Exception.Message}");
            return Task.CompletedTask;
        }
    }
}