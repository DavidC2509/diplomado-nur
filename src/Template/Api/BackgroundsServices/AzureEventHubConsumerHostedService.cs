using Azure.Messaging.EventHubs.Consumer;
using System.Text;
using System.Text.Json;
using Template.Domain.Interfaz.EventBus;
using Template.Services.EventsRecive;
using Template.Services.EventsRecive.Handler;

namespace Template.Api.BackgroundsServices
{
    public class AzureEventHubConsumerHostedService : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _serviceProvider;
        private readonly Dictionary<string, Type> _eventTypeHandlers = [];

        public AzureEventHubConsumerHostedService(IConfiguration configuration, IServiceProvider serviceProvider)
        {
            _configuration = configuration;
            _serviceProvider = serviceProvider;

            // Correctly add the type of the handler to the dictionary
            _eventTypeHandlers.Add("USER_CREATED", typeof(UserCreateReciveIntegrationEventHandler));

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var settings = _configuration.GetSection("ServiceBusSettings").Get<ServiceBusSettings>();
            var consumerGroup = EventHubConsumerClient.DefaultConsumerGroupName;

            var tasks = new List<Task>
                {
                    Task.Run(() => StartListening("cateringhub", consumerGroup, settings.ConnectionString, stoppingToken), stoppingToken)
                };

            await Task.WhenAll(tasks);
        }

        private async Task StartListening(string eventHubName, string consumerGroup, string connectionString, CancellationToken cancellationToken)
        {
            await using var consumer = new EventHubConsumerClient(consumerGroup, connectionString, eventHubName);

            await foreach (PartitionEvent partitionEvent in consumer.ReadEventsAsync(cancellationToken))
            {
                var eventData = partitionEvent.Data;
                if (eventData == null) continue;

                var message = Encoding.UTF8.GetString(eventData.Body.ToArray());

                try
                {
                    using var scope = _serviceProvider.CreateScope();

                    var integrationEvent = JsonSerializer.Deserialize<BaseModelReciveData>(
                         message,
                         new JsonSerializerOptions
                         {
                             PropertyNameCaseInsensitive = true // <- Esta línea permite mapear camelCase a PascalCase
                         });

                    if (integrationEvent is null || string.IsNullOrWhiteSpace(integrationEvent.EventType))
                        throw new InvalidOperationException("EventType is missing in message.");

                    // Buscar el handler por EventType
                    if (!_eventTypeHandlers.TryGetValue(integrationEvent.EventType, out var handlerType))
                    {
                        Console.WriteLine($"No handler found for event type: {integrationEvent.EventType}");
                        continue;
                    }

                    var handler = scope.ServiceProvider.GetRequiredService(handlerType);
                    var method = handlerType.GetMethod("MessageHandler");
                    if (method == null) throw new InvalidOperationException($"Handler for {integrationEvent.EventType} is missing MessageHandler method.");

                    var task = (Task)method.Invoke(handler, [message])!;
                    await task;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing message: {ex.Message}");
                }
            }
        }

    }
}