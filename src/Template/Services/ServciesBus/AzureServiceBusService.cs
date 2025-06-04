using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Template.Domain.Events;
using Template.Domain.Interfaz.EventBus;

namespace Template.Services.ServciesBus
{
    public class AzureServiceBusService : IEventBusService
    {
        protected ServiceBusClient _client;
        public IServiceCollection _serviceCollection;
        protected readonly IOptions<ConnectionStringConfig> _connectionStringConfig;
        private readonly List<ServiceBusSubscriptions> _handlers = new List<ServiceBusSubscriptions>();
        private readonly IServiceProvider _serviceProvider;

        public AzureServiceBusService(
            IServiceProvider serviceProvider,
            IOptions<ConnectionStringConfig> connectionStringConfig
        )
        {
            _serviceProvider = serviceProvider;
            _connectionStringConfig = connectionStringConfig;
            _client = new ServiceBusClient(connectionStringConfig.Value.AzureServiceBus);
        }

        public async Task SendMessageAsync(string TopicName, string message)
        {
            ServiceBusSender sender = _client.CreateSender(TopicName);
            var serviceBusMessage = new ServiceBusMessage(message);
            await sender.SendMessageAsync(serviceBusMessage);
        }

        public async Task Subscribe<TH>(IntegrationEvent integrationEvent) where TH : IIntegrationEventHandler
        {
            var handlerFound = _handlers.Find(h =>
                h.integrationEvent.TopicName == integrationEvent.TopicName
                && h.integrationEvent.SubscriptionName == integrationEvent.SubscriptionName
            );
            if (handlerFound != null)
            {
                return;
            }
            // create the options to use for configuring the processor
            var options = new ServiceBusProcessorOptions
            {
                // By default or when AutoCompleteMessages is set to true, the processor will complete the message after executing the message handler
                // Set AutoCompleteMessages to false to [settle messages](https://docs.microsoft.com/en-us/azure/service-bus-messaging/message-transfers-locks-settlement#peeklock) on your own.
                // In both cases, if the message handler throws an exception without settling the message, the processor will abandon the message.
                AutoCompleteMessages = false,

                // I can also allow for multi-threading
                MaxConcurrentCalls = 2
            };

            using var scope = _serviceProvider.CreateScope();
            var test = scope.ServiceProvider
            .GetRequiredService(typeof(TH)) as IIntegrationEventHandler;
            var processor = _client.CreateProcessor(integrationEvent.TopicName, integrationEvent.SubscriptionName, options);

            Func<ProcessMessageEventArgs, Task> MessageHandler = args =>
            {
                return Task.Run(() => test.MessageHandler(args));
            };

            Func<ProcessErrorEventArgs, Task> ErrorHandler = args =>
            {
                return Task.Run(() => test.ErrorHandler(args));
            };

            processor.ProcessMessageAsync += MessageHandler;
            processor.ProcessErrorAsync += ErrorHandler;

            await processor.StartProcessingAsync();

            try
            {
                // The processor performs its work in the background; block until cancellation
                // to allow processing to take place.
                await Task.Delay(Timeout.Infinite);
                await processor.StopProcessingAsync();
            }
            catch (TaskCanceledException)
            {
                // This is expected when the delay is canceled.
            }
            finally
            {
                // To prevent leaks, the handlers should be removed when processing is complete.
                processor.ProcessMessageAsync -= MessageHandler;
                processor.ProcessErrorAsync -= ErrorHandler;
            }
            // create a processor that we can use to process the messages
        }
    }

    public class ServiceBusSubscriptions
    {
        public IntegrationEvent integrationEvent;
        public Type eventHandlerType;
    }
}