using Azure.Messaging.EventHubs.Processor;

namespace Template.Domain.Interfaz.EventBus
{
    public interface IIntegrationEventHandler
    {
        Task MessageHandler(string message);
        Task ErrorHandler(ProcessErrorEventArgs args);
    };
}