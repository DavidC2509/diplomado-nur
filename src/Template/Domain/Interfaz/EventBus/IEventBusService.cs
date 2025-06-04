using Template.Domain.Events;

namespace Template.Domain.Interfaz.EventBus
{
    public interface IEventBusService
    {
        Task SendMessageAsync(string topicName, string message);

        Task Subscribe<TH>(IntegrationEvent integrationEvent)
        where TH : IIntegrationEventHandler;
    }
}