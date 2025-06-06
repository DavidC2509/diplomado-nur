using Azure.Messaging.EventHubs.Consumer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text;
using Template.Domain.Interfaz.EventBus;
using Template.Services.EventsRecive.Handler;

namespace Template.Services.ServciesBus
{
    public class AzureEventHubConsumerHostedService : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _serviceProvider;

        public AzureEventHubConsumerHostedService(IConfiguration configuration, IServiceProvider serviceProvider)
        {
            _configuration = configuration;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var settings = _configuration.GetSection("ServiceBusSettings").Get<ServiceBusSettings>();
            var consumerGroup = EventHubConsumerClient.DefaultConsumerGroupName;

            var eventHubs = new List<(string EventHubName, Type HandlerType)>
            {
                ("usert-create", typeof(UserCreateReciveIntegrationEventHandler)),
            };

            var tasks = new List<Task>();

            foreach (var (eventHubName, handlerType) in eventHubs)
            {
                tasks.Add(Task.Run(() => StartListening(eventHubName, consumerGroup, handlerType, settings.ConnectionString, stoppingToken), stoppingToken));
            }

            await Task.WhenAll(tasks);
        }

        private async Task StartListening(string eventHubName, string consumerGroup, Type handlerType, string connectionString, CancellationToken cancellationToken)
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
                    var handler = scope.ServiceProvider.GetRequiredService(handlerType);

                    var method = handlerType.GetMethod("MessageHandler");
                    if (method == null) throw new InvalidOperationException("Handler missing MessageHandler");

                    // Invocamos el handler dinámicamente
                    var task = (Task)method.Invoke(handler, [message])!;
                    await task;
                }
                catch (Exception ex)
                {
                    // Manejo de error: puedes loguear o hacer algo aquí
                    Console.WriteLine($"Error procesando mensaje: {ex.Message}");
                }
            }
        }
    }
}