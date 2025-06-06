namespace Template.Domain.Interfaz.EventBus
{
    public interface IEventBusService
    {
        Task SendMessageAsync(string eventHubName, string message);
    }
}