using Azure.Messaging.ServiceBus;

namespace Template.Domain.Interfaz.EventBus
{
    public interface IIntegrationEventHandler
    {
        Task MessageHandler(ProcessMessageEventArgs args);

        Task ErrorHandler(ProcessErrorEventArgs args);
    };

    public interface IIntegrationEventHandlersMethods
    {
        Task MessageHandler(ProcessMessageEventArgs args);

        Task ErrorHandler(ProcessErrorEventArgs args);
    }
}